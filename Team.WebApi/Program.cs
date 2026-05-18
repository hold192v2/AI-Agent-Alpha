using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Authorization;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Team.Application.Configuration;
using Team.Application.RabbitMq;
using Team.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.ConfigurePresistanceApp(builder.Configuration);
builder.Services.ConfigureApplicationApp();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = true;
        o.Audience = builder.Configuration["Authentication:Audience"];
        o.MetadataAddress = builder.Configuration["Authentication:MetadataAddress"]!;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Authentication:ValidIssuer"],
        };
        o.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        o.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var identity = context.Principal!.Identity as ClaimsIdentity;
                
                var realmAccess = context.Principal.FindFirst("realm_access");
                if (realmAccess != null)
                {
                    using var doc = JsonDocument.Parse(realmAccess.Value);
                    if (doc.RootElement.TryGetProperty("roles", out var roles))
                    {
                        foreach (var role in roles.EnumerateArray())
                        {
                            identity!.AddClaim(
                                new Claim(ClaimTypes.Role, role.GetString()!.Trim().ToLower())
                            );
                        }
                    }
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AuthCheckConsumer>();
    x.AddConsumer<RegisterConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["CloudAMQP:Url"]);
        cfg.ReceiveEndpoint("check-auth-queue", x =>
        {
            x.ConfigureConsumer<AuthCheckConsumer>(context);
            x.Bind("exchange-name");
        });
        cfg.ReceiveEndpoint("register-queue", x =>
        {
            x.ConfigureConsumer<RegisterConsumer>(context);
            x.Bind("register-name");
        });
    });
});

builder.Services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference(opt =>
{
    opt.Title = "AI Alpha Agent";
    opt.Theme = ScalarTheme.Mars;
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
