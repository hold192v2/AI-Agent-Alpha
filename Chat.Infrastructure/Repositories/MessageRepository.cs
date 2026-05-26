using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using Chat.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infrastructure.Repositories;

public class MessageRepository: IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Message>> GetMessagesByChatId(Guid chatId)
    {
        return await _context.Messages.OrderBy(c => c.ChatId == chatId).ToListAsync();
    }
}