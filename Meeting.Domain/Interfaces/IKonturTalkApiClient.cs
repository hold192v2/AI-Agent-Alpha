using Meeting.Domain.KonturEntities;

namespace Meeting.Domain.Interfaces;

public interface IKonturTalkApiClient
{
    Task<List<TalkUser>> GetUsersByMeetingId(Guid meetingId);
    Task<EmailCalendarResult> GetMeetingsByUserEmail(string userEmail);
    Task<EmailCalendarItem> FindMeetingByUserEmail(string userEmail, string id);
}