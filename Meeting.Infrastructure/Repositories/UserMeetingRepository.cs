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
    
    public async Task<List<UserMeeting>> GetUserMeetingsByUserId(Guid userId)
    {
        return await _dbContext.UserMeetings.Where(u => u.UserId == userId).ToListAsync();
    }
}