using ShortenerService.Models;

namespace ResolverService.Rabbit;

public class MessageHandler
{
    public void HandleMessage(NewShortUrlMessage message)
    {
        Console.WriteLine($"Received message with content {message.shortUrl}");
    }
}