using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkPostMaskingMode
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("employee")]
    Employee
}