using Meeting.Domain.KonturEntities;

namespace Meeting.Domain.Interfaces;

public interface IKonturTalkApiClient
{
    Task<List<TalkUser>> GetUsersByMeetingId(Guid meetingId);
}