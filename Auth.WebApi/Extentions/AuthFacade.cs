using Auth.WebApi.Extentions.Interfaces;
using DTOs;
using MassTransit;

namespace Auth.WebApi.Extentions;

public class AuthFacade :  IAuthFacade
{
    private readonly IRequestClient<UserCheckAuthRequestDto> _authClient;
    private readonly IRequestClient<RegisterIntoTeamDto> _registerClient;
    private readonly IKeycloakService _keycloakService;

    public AuthFacade(IRequestClient<UserCheckAuthRequestDto> authClient,  IRequestClient<RegisterIntoTeamDto> registerClient,  IKeycloakService keycloakService)
    {
        _authClient = authClient;
        _registerClient = registerClient;
        _keycloakService = keycloakService;
    }
    public async Task<AuthUserMeDto?> GetMeAsync(Guid userId)
    {
        var userResponse = await _authClient.GetResponse<UserCheckAuthDto>
            (new UserCheckAuthRequestDto { UserId = userId });
        if (userResponse.Message.UserId != null)
            return new AuthUserMeDto(userResponse.Message);
        
        var keycloakUser = await _keycloakService
            .GetUserAsync(userId);

        if (keycloakUser is null) return null;
        
        var patronymic = keycloakUser.Attributes?
            .GetValueOrDefault("patronymic")
            ?.FirstOrDefault();
        
        var registerResponse =
            await _registerClient.GetResponse<UserCheckAuthDto>(
                new RegisterIntoTeamDto(
                    userId,
                    keycloakUser.FirstName,
                    keycloakUser.LastName,
                    patronymic!,
                    keycloakUser.Email,
                    "teamleader"));
        if (registerResponse.Message.UserId == null)
            return null;
        return new AuthUserMeDto(registerResponse.Message);
        
    }
}