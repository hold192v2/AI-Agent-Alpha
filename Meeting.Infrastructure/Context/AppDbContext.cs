using Microsoft.EntityFrameworkCore;
using Meeting.Domain.Entities;
using Microsoft.EntityFrameworkCore.Design;

namespace Meeting.Infrastructure.Context;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
    public DbSet<Domain.Entities.Meeting> Meetings { get; set; }
    public DbSet<Protocol> Protocols { get; set; }
    public DbSet<UserMeeting> UserMeetings { get; set; }
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