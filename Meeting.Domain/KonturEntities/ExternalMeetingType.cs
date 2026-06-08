using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExternalMeetingType
{
    [JsonPropertyName("unknown")]
    Unknown,
    [JsonPropertyName("zoom")]
    Zoom,
    [JsonPropertyName("googleMeet")]
    GoogleMeet,
    [JsonPropertyName("ciscoWebex")]
    CiscoWebex,
    [JsonPropertyName("msTeams")]
    MsTeams,
    [JsonPropertyName("trueConf")]
    TrueConf,
    [JsonPropertyName("mtsLink")]
    MtsLink,
    [JsonPropertyName("iva")]
    Iva,
    [JsonPropertyName("tbankVKS")]
    TbankVks,
    [JsonPropertyName("rosbankVks")]
    RosbankVks,
    [JsonPropertyName("telemost")]
    Telemost,
    [JsonPropertyName("dion")]
    Dion,
    [JsonPropertyName("ciscoJabber")]
    CiscoJabber,
    [JsonPropertyName("custom")]
    Custom
}