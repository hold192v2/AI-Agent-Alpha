using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkSimultaneouseTranslation
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
}