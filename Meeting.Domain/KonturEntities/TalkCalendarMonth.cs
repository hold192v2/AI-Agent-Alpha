using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkCalendarMonth
{
    [JsonPropertyName("january")]
    January,
    [JsonPropertyName("february")]
    February,
    [JsonPropertyName("march")]
    March,
    [JsonPropertyName("april")]
    April,
    [JsonPropertyName("may")]
    May,
    [JsonPropertyName("june")]
    June,
    [JsonPropertyName("july")]
    July,
    [JsonPropertyName("august")]
    August,
    [JsonPropertyName("september")]
    September,
    [JsonPropertyName("october")]
    October,
    [JsonPropertyName("november")]
    November,
    [JsonPropertyName("december")]
    December
}