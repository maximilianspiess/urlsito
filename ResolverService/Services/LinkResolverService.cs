using ResolverService.Redis;

namespace ResolverService.Services;

public class LinkResolverService
{
    private readonly IRedisClient _redis;

    public LinkResolverService(IRedisClient redis)
    {
        _redis = redis;
    }

    public string? ResolveUrl(string shortUrl)
    {
        return _redis.GetEntryByShortUrl(shortUrl);
    }
}