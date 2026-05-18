using System.Security.Claims;
using System.Text.Json;
using Auth.WebApi.Extentions;
using Auth.WebApi.Extentions.Interfaces;
using DTOs;
using Keycloak.AuthServices.Authorization;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenApi("user");
builder.Services.AddScoped<IKeycloakService, KeycloakService>();
builder.Services.AddScoped<IAuthFacade, AuthFacade>();

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

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
    
    x.AddRequestClient<UserCheckAuthRequestDto>();
    x.AddRequestClient<RegisterIntoTeamDto>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["CloudAMQP:Url"]);
        cfg.Message<UserCheckAuthRequestDto>(x => x.SetEntityName("check-auth-queue"));
        cfg.Message<RegisterIntoTeamDto>(x => x.SetEntityName("register-queue"));
    });
});
builder.Services
    .AddAuthorization()
    .AddKeycloakAuthorization()
    .AddAuthorizationBuilder();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();

// Configure the HTTP request pipeline.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
        ForwardedHeaders.XForwardedHost |
        ForwardedHeaders.XForwardedProto
});
app.MapOpenApi("/openapi/{documentName}.json");
app.MapScalarApiReference(opt =>
{
    opt.Title = "AI Alpha Agent";
    opt.Theme = ScalarTheme.Mars;
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
