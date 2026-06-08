namespace Meeting.Domain.Interfaces;

public interface IMeetingRepository
{
    Task<Entities.Meeting> GetMeetingById(Guid id);
    Task CreateMeeting(Entities.Meeting meeting);
}