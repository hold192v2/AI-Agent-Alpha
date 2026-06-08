using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class EmailCalendarResult
{
    [JsonPropertyName("items")]
    public List<EmailCalendarItem> Items { get; set; }
}