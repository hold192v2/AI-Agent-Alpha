using System.Text.Json.Serialization;

namespace Chat.Domain.Entities;

public class Session
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("isReady")]
    public bool IsReady { get; set; }
    [JsonPropertyName("chatId")]
    public Guid ChatId { get; set; }
}