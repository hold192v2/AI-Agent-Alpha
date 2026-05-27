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

    public async Task addMessage(Guid chatId, Guid senderId, string content)
    {
        var message = new Message
        {
            ChatId = chatId,
            SenderId = senderId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };
        await _context.Messages.AddAsync(message);
    }

    public async Task<Message> GetAnswerByChatId(Guid chatId)
    {
        return await _context.Messages.LastOrDefaultAsync(c => c.ChatId == chatId && c.SenderId == null);
    }
}