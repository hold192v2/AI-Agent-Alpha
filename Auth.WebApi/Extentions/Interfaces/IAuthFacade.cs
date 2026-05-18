using DTOs;

namespace Auth.WebApi.Extentions.Interfaces;

public interface IAuthFacade
{
    Task<AuthUserMeDto?> GetMeAsync(Guid userId);
}