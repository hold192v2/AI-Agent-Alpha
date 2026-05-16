using System.Security.Claims;
using API_Gateway.DTOs;
using API_Gateway.Extentions.Interfaces;
using DTOs;
using Flurl.Http;
using Keycloak.Net;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Gateway.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthFacade _authFacade;
    
    public AuthController(IConfiguration config,  IAuthFacade authFacade)
    {
        _configuration = config;
        _authFacade = authFacade;
    }
    /// <param name="returnUrl">
    /// URL для перенаправления после успешной авторизации
    /// </param>
    [HttpGet("login")]
    [EndpointSummary("Авторизация пользователя через Keycloak")]
    [EndpointDescription(
        "Перенаправляет пользователя на страницу авторизации Keycloak. " + "\n" +
        "Если пользователь уже авторизован — выполняется redirect на returnUrl."
    )]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult Login([FromQuery] string? returnUrl)
    {
        if ((bool)User?.Identity?.IsAuthenticated)
            return Redirect(returnUrl ?? "https://service-desk.website.yandexcloud.net");
        
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "https://service-desk.website.yandexcloud.net"
            },
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
    [Authorize]
    [HttpGet("logout")]
    public IActionResult Logout([FromQuery] string? returnUrl)
    {
        return SignOut(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "https://socially-advantaged-moth.cloudpub.ru/gateway/login"
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult> AuthCheck()
    {
        var userId = User.GetUserId();
        var result = await _authFacade.GetMeAsync(userId);
        
        if (result is null) return Unauthorized();
        return Ok(result);
    }
}
public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirst("user-id")?.Value;

        if (!Guid.TryParse(value, out var userId))
            throw new UnauthorizedAccessException();

        return userId;
    }
}