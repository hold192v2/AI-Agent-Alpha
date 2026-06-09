using System.Text.Json.Serialization;

namespace Meeting.Domain.Entities;

public class Protocol
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("meetingId")]
    public Guid MeetingId { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; }
    [JsonPropertyName("isImproved")]
    public bool IsImproved { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; }
}