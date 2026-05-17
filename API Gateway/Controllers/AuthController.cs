using System.ComponentModel;
using System.Security.Claims;
using API_Gateway.Extentions.Interfaces;
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
    private readonly IAuthFacade _authFacade;
    
    public AuthController(IAuthFacade authFacade)
    {
        _authFacade = authFacade;
    }

    [HttpGet("login")]
    [EndpointSummary("Авторизация пользователя")]
    [EndpointDescription(
        "Перенаправляет пользователя на страницу авторизации Keycloak." + "\n" +
        "Если пользователь уже авторизован — выполняется redirect на returnUrl."
    )]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult Login(
    [Description("URL, на который будет переправлен пользователь после авторизации")]
    [FromQuery] string? returnUrl)
    {
        if ((bool)User.Identity?.IsAuthenticated)
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
    [EndpointSummary("Логаут пользователя")]
    [EndpointDescription(
        "Производит выход пользователя из учетной записи."
    )]
    [HttpGet("logout")]
    public IActionResult Logout(
        [Description("URL, на который будет переправлен пользователь после выхода из учетной записи")]
        [FromQuery] string? returnUrl)
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
    [EndpointSummary("Получение информации о пользователе")]
    [EndpointDescription(
        "Выводит информацию о пользователе для основного отображения." +
        "Если пользователя не существует в локальной базе, создает пользователя на основе информации из Keycloak."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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