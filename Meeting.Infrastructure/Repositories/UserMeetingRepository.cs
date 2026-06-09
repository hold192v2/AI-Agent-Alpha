using Meeting.Domain.Entities;
using Meeting.Domain.Interfaces;
using Meeting.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Meeting.Infrastructure.Repositories;

public class UserMeetingRepository: IUserMeetingRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserMeetingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Guid>> GetMeetingIdsByUserId(Guid userId)
    {
        return await _dbContext.UserMeetings.Where(u => u.UserId == userId).Select(u => u.MeetingId).ToListAsync();
    }
    
    public async Task<List<Guid>> GetUserIdsByMeetingId(Guid meetingId)
    {
        return await _dbContext.UserMeetings.Where(u => u.UserId == meetingId).Select(u => u.UserId).ToListAsync();
    }
}