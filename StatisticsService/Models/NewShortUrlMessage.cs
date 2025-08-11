namespace StatisticsService.Models;

public class NewShortUrlMessage
{
    public required string LongUrl { get; init; }
    public required string ShortUrl { get; init; }
}