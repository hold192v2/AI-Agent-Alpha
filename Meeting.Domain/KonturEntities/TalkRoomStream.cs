using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkRoomStream
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("expirationDate")]
    public string ExpirationDate { get; set; }
    [JsonPropertyName("allowAnonymous")]
    public bool AllowAnonymous { get; set; }
    [JsonPropertyName("isEnabled")]
    public bool IsEnabled { get; set; }
    [JsonPropertyName("isActive")]
    public bool IsActive { get; set; }
    [JsonPropertyName("streamKey")]
    public string StreamKey { get; set; }
    [JsonPropertyName("viewersCount")]
    public int ViewersCount { get; set; }
    [JsonPropertyName("playListSource")]
    public string PlayListSource { get; set; }
    [JsonPropertyName("playlistId")]
    public string PlaylistId { get; set; }
}