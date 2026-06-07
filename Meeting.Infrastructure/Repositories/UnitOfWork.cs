using Meeting.Domain.Interfaces;
using Meeting.Infrastructure.Context;

namespace Meeting.Infrastructure.Repositories;

public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task Commit(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync();
    }
}