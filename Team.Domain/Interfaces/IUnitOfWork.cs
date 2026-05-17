namespace Team.Domain.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}