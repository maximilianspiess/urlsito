namespace ShortenerService.Models;

public class NewShortUrlMessage
{
    public required string longUrl { get; init; }
    public required string shortUrl { get; init; }
}