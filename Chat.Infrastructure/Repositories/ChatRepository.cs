using Chat.Domain.Interfaces;
using Chat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class ChatRepository: IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Task<List<Domain.Entities.Chat>> GetChatsByUserId(Guid userId)
    {
        return _context.Chats.OrderBy(c => c.UserId == userId).ToListAsync();
    }
}