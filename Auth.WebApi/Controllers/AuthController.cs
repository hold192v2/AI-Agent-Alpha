using System.Security.Claims;
using Auth.WebApi.Extentions.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApi.Controllers;

[ApiController]
[Route("user")]
public class AuthController : ControllerBase
{
    private readonly IAuthFacade _authFacade;
    
    public AuthController(IAuthFacade authFacade)
    {
        _authFacade = authFacade;
    }
    [Authorize]
    [EndpointSummary("Получение информации о пользователе")]
    [EndpointDescription(
        "Выводит информацию о пользователе для основного отображения." +
        "Если пользователя не существует в локальной базе, создает пользователя на основе информации из Keycloak."
    )]
    [ProducesResponseType(typeof(AuthUserMeDto),StatusCodes.Status200OK)]
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
        return Guid.Parse(
            user.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
            ?? throw new UnauthorizedAccessException()
        );
    }
}