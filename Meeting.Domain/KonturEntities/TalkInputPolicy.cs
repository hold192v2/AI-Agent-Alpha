using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkInputPolicy
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("muted")]
    Muted,
    [JsonPropertyName("disabled")]
    Disabled
}