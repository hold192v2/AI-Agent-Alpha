using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class Participant
{
    [JsonPropertyName("participantId")]
    public Guid ParticipantId { get; set; }
    [JsonPropertyName("participantName")]
    public string ParticipantName { get; set; }
    [JsonPropertyName("isGuest")]
    public bool IsGuest { get; set; }
    [JsonPropertyName("connectionsInfo")]
    public List<TalkConferenceReportParticipantConnectionInfo> ConnectionsInfo { get; set; }
}