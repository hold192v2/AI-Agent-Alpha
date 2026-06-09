using Meeting.Domain.Entities;

namespace Meeting.Domain.Interfaces;

public interface IProtocolRepository
{
    Task<List<Protocol>> GetProtocolsByMeetingId(Guid meetingId);
    Task<Protocol?> GetProtocolById(Guid id);
    Task CreateProtocol(Protocol protocol);
    Task UpdateProtocol(Protocol protocol);
    Task<List<Guid>> GetMeetingIdsByProtocolId(Guid protocolId);
}