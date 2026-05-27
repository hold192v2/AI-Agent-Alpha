using Chat.Domain.Interfaces;
using Chat.Domain.Entities;
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

    public async Task<Domain.Entities.Chat> GetChatById(Guid? chatId)
    {
        return await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
    }

    public async Task CreateChat(Domain.Entities.Chat chat)
    {
        await _context.Chats.AddAsync(chat);
    }
}