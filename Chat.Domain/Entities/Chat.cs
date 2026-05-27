using System.Text.Json.Serialization;

namespace Chat.Domain.Entities;

public class Chat
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("teamId")]
    public Guid? TeamId { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("role")]
    public string Role { get; set; }
}