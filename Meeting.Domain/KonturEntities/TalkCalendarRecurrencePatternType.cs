using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkCalendarRecurrencePatternType
{
    [JsonPropertyName("notSupported")]
    NotSupported,
    [JsonPropertyName("daily")]
    Daily,
    [JsonPropertyName("dailyRegeneration")]
    DailyRegeneration,
    [JsonPropertyName("weekly")]
    Weekly,
    [JsonPropertyName("weeklyRegeneration")]
    WeeklyRegeneration,
    [JsonPropertyName("monthly")]
    Monthly,
    [JsonPropertyName("monthlyRegeneration")]
    MonthlyRegeneration,
    [JsonPropertyName("relativeMonthly")]
    RelativeMonthly,
    [JsonPropertyName("yearly")]
    Yearly,
    [JsonPropertyName("relativeYearly")]
    RelativeYearly,
    [JsonPropertyName("yearlyRegeneration")]
    YearlyRegeneration
}