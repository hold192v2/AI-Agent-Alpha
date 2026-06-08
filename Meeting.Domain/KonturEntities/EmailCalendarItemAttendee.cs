using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class EmailCalendarItemAttendee
{
    [JsonPropertyName("user")]
    public TalkUser User { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("mailBox")]
    public string MailBox { get; set; }
    [JsonPropertyName("type")]
    public AttendeeType Type { get; set; }
    [JsonPropertyName("status")]
    public AttendeeStatus Status { get; set; }
}