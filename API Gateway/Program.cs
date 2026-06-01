using API_Gateway.Extentions;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Yarp_API_Gateway.Extentions;
using Yarp.ReverseProxy.Transforms.Builder;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi("gateway", opt =>
{
    opt.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "Gateway API";
        document.Info.Version = "v1";
        document.Components ??= new();
        document.Components.SecuritySchemes ??=
            new Dictionary<string, IOpenApiSecurityScheme>();

        document.Components.SecuritySchemes["Keycloak"] =
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    ClientCredentials = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["Keycloak:AuthorizationUrl"]!),

                        TokenUrl = new Uri(builder.Configuration["Keycloak:TokenUrl"]!),

                        Scopes = new Dictionary<string, string>
                        {
                            ["openid"] = "openid",
                            ["profile"] = "profile",
                        }
                    },
                }
                
            };
        document.Security ??= new List<OpenApiSecurityRequirement>();
        
        document.Security.Add(new OpenApiSecurityRequirement
        {
            [
                new OpenApiSecuritySchemeReference("Keycloak")
            ] = ["openid"]
        });
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
        options.PushedAuthorizationBehavior =
            PushedAuthorizationBehavior.Disable;
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
        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "Cookies");
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

app.MapOpenApi("/openapi/{documentName}.json").AllowAnonymous();
app.MapScalarApiReference(opt =>
{
    opt.Title = "AI Alpha Agent";
    opt.Theme = ScalarTheme.Mars;
    opt.AddDocument("Gateway", "Gateway");
    opt.AddDocument("User", "User", "https://doggedly-succinct-ridgeback.cloudpub.ru/user/openapi/user.json");
    opt.AddDocument("Team", "Team", "https://doggedly-succinct-ridgeback.cloudpub.ru/team/openapi/team.json");
    opt.AddDocument("Chat", "Chat", "https://doggedly-succinct-ridgeback.cloudpub.ru/chat/openapi/chat.json");
    opt.AddDocument("Meeting", "Meeting", "https://doggedly-succinct-ridgeback.cloudpub.ru/meeting/openapi/meeting.json");
    opt.AddDocument("Metrics", "Metrics", "https://doggedly-succinct-ridgeback.cloudpub.ru/metrics/openapi/metrics.json");
    opt.WithTitle("API Gateway")
        .AddPreferredSecuritySchemes("Keycloak")
        .AddClientCredentialsFlow("Keycloak", scheme =>
    {
        scheme.ClientId = builder.Configuration["Keycloak:ClientId"];
        scheme.ClientSecret = builder.Configuration["Keycloak:ClientSecret"];
    });
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
app.MapReverseProxy();

app.UseHttpsRedirection();

app.Run();

