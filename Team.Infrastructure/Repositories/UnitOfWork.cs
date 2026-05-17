using Team.Domain.Interfaces;

namespace Team.Infrastructure.Repositories;

public class UnitOfWork :  IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}