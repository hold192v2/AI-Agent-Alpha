using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkConferenceReportParticipantConnectionInfo
{
    [JsonPropertyName("appPlatform")]
    public string AppPlatform { get; set; }
    [JsonPropertyName("isViaProxy")]
    public bool IsViaProxy { get; set; }
    [JsonPropertyName("participantRealIp")]
    public string ParticipantRealIp { get; set; }
    [JsonPropertyName("country")]
    public string Country { get; set; }
}