using Meeting.Domain.KonturEntities;

namespace Meeting.Domain.Interfaces;

public interface IKonturTalkApiClient
{
    Task<EmailCalendarResult> GetMeetingsByUserEmail(string userEmail, DateTime start, DateTime? end, int? take);
    Task<EmailCalendarItem> FindMeetingByUserEmailAndId(string userEmail, string id);
}