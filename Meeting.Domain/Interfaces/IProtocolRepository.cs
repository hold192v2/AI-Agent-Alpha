using Meeting.Domain.Entities;

namespace Meeting.Domain.Interfaces;

public interface IProtocolRepository
{
    Task<List<Protocol>> GetProtocolsByMeetingId(Guid meetingId);
}