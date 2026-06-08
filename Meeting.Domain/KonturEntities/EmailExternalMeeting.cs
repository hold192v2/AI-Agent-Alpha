using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class EmailExternalMeeting
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
    [JsonPropertyName("type")]
    public ExternalMeetingType Type { get; set; }
    [JsonPropertyName("vcsName")]
    public string VcsName { get; set; }
}