
using MongoDB.Driver;
using ShortenerService.Mongo;
using ShortenerService.Repository;

var builder = WebApplication.CreateBuilder(args);
// MongoDB config
var mongoDbSettings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>();
var client = new MongoClient(mongoDbSettings.ConnectionString);
var database = client.GetDatabase(mongoDbSettings.DatabaseName);

// Register MongoDb context
builder.Services.AddSingleton<IMongoDatabase>(database);

// Register generic repo for dependency injection
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

// Add services to the container.
builder.Services.AddGrpc();
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