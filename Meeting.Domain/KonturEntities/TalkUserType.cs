using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkUserType
{
    [JsonPropertyName("normal")]
    Normal,
    [JsonPropertyName("kiosk")]
    Kiosk,
    [JsonPropertyName("guest")]
    Guest
}