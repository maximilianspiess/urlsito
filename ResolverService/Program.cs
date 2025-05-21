using Microsoft.AspNetCore.Server.Kestrel.Core;
using ResolverService.Rabbit;
using ResolverService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the LinkResolverService
builder.Services.AddScoped<LinkResolverService>();

// Register background rabbitMQ receiver and handler
builder.Services.AddTransient<MessageHandler>();
builder.Services.AddHostedService<Receiver>();

// Add services to the container.
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5050, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();


app.Run();