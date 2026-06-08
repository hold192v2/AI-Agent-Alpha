using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkCalendarRecurrence
{
    [JsonPropertyName("type")]
    public TalkCalendarRecurrencePatternType Type { get; set; }
    [JsonPropertyName("startDate")]
    public DateTime StartDate { get; set; }
    [JsonPropertyName("endDate")]
    public DateTime EndDate { get; set; }
    [JsonPropertyName("numberOjEvents")]
    public int NumberOjEvents { get; set; }
    [JsonPropertyName("interval")]
    public int Interval { get; set; }
    [JsonPropertyName("dayOfMonth")]
    public int DayOfMonth { get; set; }
    [JsonPropertyName("daysOfTheWeek")]
    public List<TalkCalendarDayOfTheWeek>  DaysOfTheWeek { get; set; }
    [JsonPropertyName("month")]
    public TalkCalendarMonth Month { get; set; }
    [JsonPropertyName("dayOfWeekIndex")]
    public TalkCalendarDayOfTheWeekIndex DayOfWeekIndex { get; set; }
    [JsonPropertyName("firstEventId")]
    public string FirstEventId { get; set; }
}