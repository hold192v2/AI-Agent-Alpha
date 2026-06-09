using Meeting.Domain.Entities;

namespace Meeting.Domain.Interfaces;

public interface IUserMeetingRepository
{
    Task<List<UserMeeting>> GetUserMeetingsByUserId(Guid userId);
}