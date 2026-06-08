using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkAnonymouss
{
    [JsonPropertyName("anonymousId")]
    public string AnonymousId { get; set; }
    [JsonPropertyName("anonymousName")]
    public string AnonymousName { get; set; }
}