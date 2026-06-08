using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AttendeeStatus
{
    [JsonPropertyName("unknown")]
    Unknown,
    [JsonPropertyName("accepted")]
    Accepted,
    [JsonPropertyName("rejected")]
    Rejected
}