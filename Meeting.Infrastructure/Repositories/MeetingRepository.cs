using AutoMapper;
using Meeting.Domain.Interfaces;
using Meeting.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Infrastructure.Repositories;

public class MeetingRepository: IMeetingRepository
{
    public readonly AppDbContext _dbContext;
    private IMeetingRepository _meetingRepositoryImplementation;

    public MeetingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Entities.Meeting> GetMeetingById(Guid id)
    {
        return await _dbContext.Meetings.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task CreateMeeting(Domain.Entities.Meeting meeting)
    {
        await _dbContext.Meetings.AddAsync(meeting);
    }
}