using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Chat.Domain.Entities;

namespace Chat.Infrastructure.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    public DbSet<Domain.Entities.Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}

public class YourDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=chat_service;Username=postgres;Password=second");
        
        return new AppDbContext(optionsBuilder.Options);
    }
}