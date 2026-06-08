using Meeting.Domain.Entities;
using Meeting.Domain.Interfaces;
using Meeting.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Infrastructure.Repositories;

public class ProtocolRepository: IProtocolRepository
{
    private readonly AppDbContext _dbContext;
    private IProtocolRepository _protocolRepositoryImplementation;

    public ProtocolRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Protocol>> GetProtocolsByMeetingId(Guid meetingId)
    {
        return await _dbContext.Protocols.Where(p => p.MeetingId == meetingId).ToListAsync();
    }

    public async Task<Protocol> GetProtocolById(Guid id)
    {
        return await _dbContext.Protocols.FirstOrDefaultAsync(p => p.Id == id);
    }
}