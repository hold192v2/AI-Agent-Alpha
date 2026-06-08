using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkCalendarDayOfTheWeekIndex
{
    [JsonPropertyName("first")]
    First,
    [JsonPropertyName("second")]
    Second,
    [JsonPropertyName("third")]
    Third,
    [JsonPropertyName("fourth")]
    Fourth,
    [JsonPropertyName("last")]
    Last
}