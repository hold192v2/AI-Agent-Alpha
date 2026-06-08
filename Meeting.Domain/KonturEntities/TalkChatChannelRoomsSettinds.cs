using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkChatChannelRoomsSettinds
{
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
    [JsonPropertyName("allowedUsers")]
    public List<TalkUserRef> AllowedUsers { get; set; }
}