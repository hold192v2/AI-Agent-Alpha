using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Team.Domain.Entities;

namespace Team.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    
    public DbSet<User> Users { get; set; }
}

public class YourDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ai_agent;Username=postgres;Password=second");

        return new AppDbContext(optionsBuilder.Options);
    }
}