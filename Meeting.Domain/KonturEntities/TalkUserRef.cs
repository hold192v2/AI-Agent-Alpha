using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkUserRef
{
    [JsonPropertyName("anonymousName")]
    public string AnonymousName { get; set; }
    [JsonPropertyName("anonymousId")]
    public string AnonymousId { get; set; }
    [JsonPropertyName("userInfo")]
    public TalkUser UserInfo { get; set; }
    [JsonPropertyName("isAnonymous")]
    public bool IsAnonymous { get; set; }
}