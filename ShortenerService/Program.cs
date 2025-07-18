
using MongoDB.Driver;
using ShortenerService.Mongo;
using ShortenerService.Rabbit;
using ShortenerService.Repository;
using ShortenerService.Services;

var builder = WebApplication.CreateBuilder(args);
// MongoDB config
var mongoDbSettings = builder.Configuration.GetRequiredSection("MongoDb").Get<MongoDbSettings>();
var client = new MongoClient(mongoDbSettings.ConnectionString);
var database = client.GetDatabase(mongoDbSettings.DatabaseName);

// Register MongoDb context
builder.Services.AddSingleton(database);
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

// Register generic repo for dependency injection
builder.Services.AddTransient<IShortLinkRepository, ShortLinkRepository>();

// Register LinkShortenerService
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddScoped<LinkShortenerService>();

// Register RabbitMQ Sender
builder.Services.AddScoped<ISender, Sender>();

// Add services to the container.
builder.Services.AddControllers();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5040, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();


app.Run();