using DTOs;
using Keycloak.Net.Models.Users;

namespace API_Gateway.Extentions.Interfaces;

public interface IKeycloakService
{
    Task<User> GetUserAsync(Guid userId);
}