using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class RoomSipSettings
{
    [JsonPropertyName("sipNumber")]
    public string SipNumber { get; set; }
    [JsonPropertyName("sipUrl")]
    public string SipUrl { get; set; }
    [JsonPropertyName("phoneNumbers")]
    public List<string> PhoneNumbers { get; set; }
}