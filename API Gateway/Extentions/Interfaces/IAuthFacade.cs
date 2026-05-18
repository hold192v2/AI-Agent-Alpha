using DTOs;

namespace API_Gateway.Extentions.Interfaces;

public interface IAuthFacade
{
    Task<AuthUserMeDto?> GetMeAsync(Guid userId);
}