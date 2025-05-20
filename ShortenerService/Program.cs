
using ShortenerService.Mongo;
using ShortenerService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<IShortLinkRepository, ShortLinkRepository>();
builder.Services.AddScoped<ShortenerService.Services.ShortenerService>();

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