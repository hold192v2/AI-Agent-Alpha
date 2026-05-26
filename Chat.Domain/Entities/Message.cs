using System.Text.Json.Serialization;

namespace Chat.Domain.Entities;

public class Message
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("chatId")]
    public Guid ChatId { get; set; }
    [JsonPropertyName("senderId")]
    public string SenderId { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; }
    [JsonPropertyName("digestKey")]
    public string? DigestKey { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}