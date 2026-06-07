using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkUserStatus
{
    [JsonPropertyName("unknown")]
    Unknown,
    [JsonPropertyName("inMeeting")]
    InMeeting,
    [JsonPropertyName("doNotDisturb")]
    DoNotDisturb,
    [JsonPropertyName("online")]
    Online
}