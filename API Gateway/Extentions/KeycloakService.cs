using API_Gateway.DTOs;
using API_Gateway.Extentions.Interfaces;
using DTOs;
using Flurl.Http;
using Keycloak.Net;
using Keycloak.Net.Models.Users;
using Microsoft.Extensions.Caching.Memory;

namespace API_Gateway.Extentions;

public class KeycloakService : IKeycloakService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;

    public KeycloakService(IConfiguration configuration, IMemoryCache cache)
    {
        _configuration = configuration;
        _cache = cache;
    }
    
    public async Task<User> GetUserAsync(Guid userId)
    {
        var token = await GetAdminTokenAsync();

        var keycloak = new KeycloakClient(
            "https://auth.alpha-helpdesk.ru",
            () => token);

        try
        {
            return await keycloak.GetUserAsync(
                _configuration["Keycloak:Realm"]!,
                userId.ToString());
        }
        catch
        {
            return null!;
        }
    }
    private async Task<string> GetAdminTokenAsync()
    {
        const string cacheKey = "kc_admin_token";

        if (_cache.TryGetValue(cacheKey, out string? cachedToken))
            return cachedToken!;

        var tokenResponse = await _configuration["Keycloak:TokenUrl"]
            .PostUrlEncodedAsync(new
            {
                grant_type = "client_credentials",
                client_id = _configuration["Keycloak:ClientId"],
                client_secret = _configuration["Keycloak:ClientSecret"]
            })
            .ReceiveJson<SessionDto>();

        var token = tokenResponse.GetAccessToken();

        _cache.Set(cacheKey, token, TimeSpan.FromMinutes(5));
        
        return token;
    }
}