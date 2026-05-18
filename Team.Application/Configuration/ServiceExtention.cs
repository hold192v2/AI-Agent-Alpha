using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Team.Application.Mappers;

namespace Team.Application.Configuration;

public static class ServiceExtention
{
    public static void ConfigureApplicationApp(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(UserCheckAuthMapper).Assembly);
    }
}