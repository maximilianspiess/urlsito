using ResolverService.Redis;
using ShortenerService.Models;

namespace ResolverService.Rabbit;

public class MessageHandler
{
    private readonly IRedisClient _redis;

    public MessageHandler(IRedisClient redis)
    {
        _redis = redis;
    }
    public void HandleMessage(NewShortUrlMessage message)
    {
        Console.WriteLine($"Received message with content {message.shortUrl}");
        _redis.SaveEntry(message);
    }
}