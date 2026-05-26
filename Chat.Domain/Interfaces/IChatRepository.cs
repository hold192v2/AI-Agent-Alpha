using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces;

public interface IChatRepository
{
    Task<List<Entities.Chat>> GetChatsByUserId(Guid userId);
}