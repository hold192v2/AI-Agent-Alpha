using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Authorization;
using MassTransit;
using Meet.Application.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenApi("meeting");

builder.Services.AddControllers();

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
    
    x.AddRequestClient<UsersByIdRequest>();
    x.AddRequestClient<EmailByUsersIdRequest>();
    x.AddRequestClient<OldProtocolDesc>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["CloudAMQP:Url"]);
        cfg.Message<UsersByIdRequest>(x => x.SetEntityName("users-by-id-queue"));
        cfg.Message<EmailByUsersIdRequest>(x => x.SetEntityName("email-by-id-queue"));
        cfg.Message<OldProtocolDesc>(x => x.SetEntityName("protocol-formalize-queue"));
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
