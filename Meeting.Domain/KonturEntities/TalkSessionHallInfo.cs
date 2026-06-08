using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkSessionHallInfo
{
    [JsonPropertyName("conferenceId")]
    public string ConferenceId { get; set; }
    [JsonPropertyName("chatRoom")]
    public string ChatRoom { get; set; }
    [JsonPropertyName("onlineUsersCount")]
    public int OnlineUsersCount { get; set; }
    [JsonPropertyName("onlineUsers")]
    public List<TalkUserRef> OnlineUsers { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("users")]
    public List<TalkUserRef> Users { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("color")]
    public string Color { get; set; }
}