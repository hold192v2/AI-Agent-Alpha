using System.Text.Json.Serialization;

namespace API_Gateway.DTOs;

public class SessionDto
{
    public SessionDto(string accessToken, string refreshToken, string idToken, long expiresAt)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ExpiresAt = expiresAt;
        IdToken = idToken;
    }
    
    public string UserId { get; init; }
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; init; }
    [JsonPropertyName("id_token")]
    public string IdToken { get; init; } 
    [JsonPropertyName("expires_in")]
    public long ExpiresAt { get; init; }

    public string GetAccessToken() => AccessToken;


}