using System.Text.Json.Serialization;

namespace Meeting.Domain.Entities;

public class Meeting
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("konturMeetingId")]
    public string KonturMeetingId { get; set; }
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}