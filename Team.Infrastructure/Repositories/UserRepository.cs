using Microsoft.EntityFrameworkCore;
using Team.Domain.Entities;
using Team.Domain.Interfaces;

namespace Team.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext appDbContext) 
    {
        _context = appDbContext;
    }
    
    public async Task<User> GetUserByUserId(Guid id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
    }
}