
using ResolverService.Rabbit;
using StatisticsService.Clickhouse;
using StatisticsService.Services;

var builder = WebApplication.CreateBuilder(args);

// Register Clickhouse Settings
builder.Services.Configure<ClickhouseSettings>(builder.Configuration.GetSection("Clickhouse"));
builder.Services.AddScoped<IClickhouseClient, ClickhouseClient>();

// Register RabbitMQ Receiver
builder.Services.AddHostedService<Receiver>();

// Register Message converter
builder.Services.AddTransient<IMessageConverter, MessageConverter>();


// Add services to the container
builder.Services.AddScoped<IStatisticsService, StatisticsService.Services.StatisticsService>();

// Add controllers to the container.
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5060, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();


app.Run();