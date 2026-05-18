using API_Gateway.Extentions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Scalar.AspNetCore;
using Yarp_API_Gateway.Extentions;
using Yarp.ReverseProxy.Transforms.Builder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi("gateway", opt =>
{
    opt.OpenApiVersion =
        Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;

    opt.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Gateway API";
        document.Info.Version = "v1";

        return Task.CompletedTask;
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddSingleton<ITransformProvider, AccessTokenTransformProvider>();

builder.Services.AddHttpClient("AllowAnyCert")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = 
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

    })
    .AddCookie("Cookies", options =>
        {
            options.Cookie.Name = ".Gateway.Auth";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.None; 
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            
            options.Events = new CookieAuthenticationEvents
            {
                OnRedirectToLogin = ctx =>
                {
                    ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                },
                OnRedirectToAccessDenied = ctx =>
                {
                    ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                }
            };
        }
    )
    .AddOpenIdConnect("OpenIdConnect", options =>
    {   
        options.RequireHttpsMetadata = true;
        options.Authority = builder.Configuration["Authentication:Authority"];
        options.ClientId = builder.Configuration["Keycloak:ClientId"];
        options.ClientSecret = builder.Configuration["Keycloak:ClientSecret"];
        options.ResponseType = "code";
        options.SaveTokens = true; 
        options.CallbackPath = "/signin-oidc";
        options.SignedOutCallbackPath = "/signout-callback-oidc";
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["Authentication:ValidIssuer"];
        options.Audience = builder.Configuration["Authentication:Audience"];

        options.BackchannelHttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiPolicy", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddAuthenticationSchemes(
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    });
});

builder.Services.AddControllers();

builder.Services
    .AddReverseProxy()
    .ConfigureHttpClient((context, handler) =>
    {
        handler.SslOptions.RemoteCertificateValidationCallback =
            (_, _, _, _) => true;
    })
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi("/openapi/{documentName}.json");
app.MapScalarApiReference(opt =>
{
    opt.Title = "AI Alpha Agent";
    opt.Theme = ScalarTheme.Mars;
    opt.AddDocument("Gateway", "Gateway");
    opt.AddDocument("User", "User", "https://localhost:7147/user/openapi/user.json");
    opt.AddDocument("Team", "Team", "https://localhost:7147/team/openapi/team.json");
    opt.AddDocument("Chat", "Chat", "https://localhost:7147/chat/openapi/chat.json");
    opt.AddDocument("Meeting", "Meeting", "https://localhost:7147/meeting/openapi/meeting.json");
});
app.UseCors("FrontendPolicy");
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto
});
app.UseAuthentication().UseAuthorization();
app.MapControllers();
app.MapReverseProxy().RequireAuthorization("ApiPolicy");

app.UseHttpsRedirection();

app.Run();

