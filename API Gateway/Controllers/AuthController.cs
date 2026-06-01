using System.ComponentModel;
using System.Security.Claims;
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
            return Redirect(returnUrl ?? "https://doggedly-succinct-ridgeback.cloudpub.ru/");
        
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl ?? "https://doggedly-succinct-ridgeback.cloudpub.ru/"
            },
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
    [Authorize]
    [EndpointSummary("Выход пользователя")]
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
                RedirectUri = returnUrl ?? "https://doggedly-succinct-ridgeback.cloudpub.ru/auth/login"
            },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
    }
}