using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkLanguagePair
{
    [JsonPropertyName("from")]
    public string From { get; set; }
    [JsonPropertyName("to")]
    public string To { get; set; }
    [JsonPropertyName("translators")]
    public List<TalkUserRef> Translators { get; set; }
}