using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Context;

namespace Chat.Infrastructure.Repositories;

public class SessionRepository: ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Session> GetSessionById(Guid id)
    {
        return _context.Sessions.FirstOrDefault(s => s.Id == id);
    }

    public async Task CreateSession(Session session)
    {
        await _context.Sessions.AddAsync(session);
    }
}