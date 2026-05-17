using Team.Domain.Entities;

namespace Team.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUserId(Guid id);
    public Task CreateUser(User user);
}