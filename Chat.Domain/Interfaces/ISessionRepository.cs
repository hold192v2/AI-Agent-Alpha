using Chat.Domain.Entities;

namespace Chat.Domain.Interfaces;

public interface ISessionRepository
{
    Task<Session> GetSessionById(Guid id);
    Task CreateSession(Session session);
}