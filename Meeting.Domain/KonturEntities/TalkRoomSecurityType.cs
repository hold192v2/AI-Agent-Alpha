using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkRoomSecurityType
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("pinCode")]
    PinCode
}