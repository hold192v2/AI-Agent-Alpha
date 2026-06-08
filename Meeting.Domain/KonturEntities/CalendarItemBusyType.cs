using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CalendarItemBusyType
{
    [JsonPropertyName("free")] 
    Free,
    [JsonPropertyName("tentative")]
    Tentative,
    [JsonPropertyName("busy")]
    Busy,
    [JsonPropertyName("outOfOffice")]
    OutOfOffice,
    [JsonPropertyName("workingElsewhere")]
    WorkingElsewhere,
    [JsonPropertyName("noData")]
    NoData
}