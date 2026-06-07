using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkRoleInfo
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}