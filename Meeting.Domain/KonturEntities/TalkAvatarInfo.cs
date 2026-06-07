using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkAvatarInfo
{
    [JsonPropertyName("width")]
    public int Width { get; set; }
    
    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("face")]
    public TalkFaceRectangle Face { get; set; }

    [JsonPropertyName("contentHash")]
    public string ContentHash { get; set; }
}