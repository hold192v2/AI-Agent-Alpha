using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkCalendarDayOfTheWeek
{
    [JsonPropertyName("sunday")]
    Sunday,
    [JsonPropertyName("monday")]
    Monday,
    [JsonPropertyName("tuesday")]
    Tuesday,
    [JsonPropertyName("wednesday")]
    Wednesday,
    [JsonPropertyName("thursday")]
    Thursday,
    [JsonPropertyName("friday")]
    Friday,
    [JsonPropertyName("saturday")]
    Saturday,
    [JsonPropertyName("day")]
    Day,
    [JsonPropertyName("weekday")]
    Weekday,
    [JsonPropertyName("weekendDay")]
    WeekendDay
}