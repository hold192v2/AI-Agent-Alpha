using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkMaskingSettings
{
    [JsonPropertyName("nameMaskingMode")]
    public TalkNameMaskingMode NameMaskingMode { get; set; }
    [JsonPropertyName("postMaskingMode")]
    public TalkPostMaskingMode PostMaskingMode { get; set; }
    [JsonPropertyName("showAdditionalInfo")]
    public bool ShowAdditionalInfo { get; set; }
}