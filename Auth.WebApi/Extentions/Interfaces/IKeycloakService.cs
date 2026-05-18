using Keycloak.Net.Models.Users;

namespace Auth.WebApi.Extentions.Interfaces;

public interface IKeycloakService
{
    Task<User> GetUserAsync(Guid userId);
}