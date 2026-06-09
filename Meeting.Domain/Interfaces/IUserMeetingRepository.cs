using Meeting.Domain.Entities;

namespace Meeting.Domain.Interfaces;

public interface IUserMeetingRepository
{
    Task<List<Guid>> GetMeetingIdsByUserId(Guid userId);
    Task<List<Guid>> GetUserIdsByMeetingId(Guid meetingId);
}