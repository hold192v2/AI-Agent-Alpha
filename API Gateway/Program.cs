using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi(opt =>
{
    opt.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "AI Alpha Agent";
        document.Info.Description = "Проект для авторизации";

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
builder.Services.AddMemoryCache();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapScalarApiReference(opt =>
{
    opt.Title = "AI Alpha Agent";
    opt.Theme = ScalarTheme.Mars;
});

app.UseCors("FrontendPolicy");

app.UseHttpsRedirection();

app.Run();

