using System.Text.Json.Serialization;

namespace Chat.Domain.Entities;

public class Chat
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("teamId")]
    public string? TeamId { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
}