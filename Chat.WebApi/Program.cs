using System.Security.Claims;
using System.Text.Json;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOpenApi("chat", config =>
{
    config.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Сервис управления чатов";
        document.Info.Description = "Используется для вывода истории обращений, а также начальной обработки сообщений, задаваемых пользователем.";
        document.Info.Version = "v1";
        
        return Task.CompletedTask;
    });
});

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
    opt.Title = "Сервис управления чатов";
    opt.Theme = ScalarTheme.Mars;
});

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();
