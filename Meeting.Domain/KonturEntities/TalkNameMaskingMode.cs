using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TalkNameMaskingMode
{
    [JsonPropertyName("none")]
    None,
    [JsonPropertyName("nameAndFirstLetterOfSurname")]
    NameAndFirstLetterOfSurname,
    [JsonPropertyName("nameOnly")]
    NameOnly
}