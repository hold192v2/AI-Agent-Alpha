using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces;

public interface IMessageRepository
{
    Task<List<Message>> GetMessagesByChatId(Guid chatId);
    Task AddMessage();
}