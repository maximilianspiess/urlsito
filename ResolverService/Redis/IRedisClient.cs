using ShortenerService.Models;

namespace ResolverService.Redis;

public interface IRedisClient
{
    public void SaveEntry(NewShortUrlMessage urlMessage);

    public string? GetEntryByShortUrl(string shortUrl);
}