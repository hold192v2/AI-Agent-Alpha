using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Team.Domain.Interfaces;
using Team.Infrastructure.Repositories;

namespace Team.Infrastructure;

public static class ServiceExtentions
{
    public static void ConfigurePresistanceApp(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("postgres");
        IServiceCollection serviceCollection = services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString, x => x.MigrationsAssembly("Infrastructure.Infrastructure")), ServiceLifetime.Scoped);
        
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}