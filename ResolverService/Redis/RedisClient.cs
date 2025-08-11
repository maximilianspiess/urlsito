using Microsoft.Extensions.Options;
using ShortenerService.Models;
using StackExchange.Redis;

namespace ResolverService.Redis;

public class RedisClient: IRedisClient
{
    private readonly ConnectionMultiplexer _redis;
    private readonly IDatabase _database;
    private RedisSettings _settings;

    public RedisClient(IOptions<RedisSettings> settings)
    {
        _settings = settings.Value;
        _redis = ConnectionMultiplexer.Connect(_settings.Host + ":" + _settings.Port);
        _database = _redis.GetDatabase();
    }
        
    public void SaveEntry(NewShortUrlMessage urlMessage)
    {
        _database.StringSet(urlMessage.shortUrl, urlMessage.longUrl);
    }

    public string? GetEntryByShortUrl(string shortUrl)
    {
        var entry = _database.StringGet(shortUrl);
        return entry.HasValue ? entry.ToString() : null;
    }
}