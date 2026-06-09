using System.Text.Json.Serialization;

namespace Meeting.Domain.Entities;

public class UserMeeting
{
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("meetingId")]
    public Guid MeetingId { get; set; }
}