using StatisticsService.Clickhouse;
using StatisticsService.Models;
using StatisticsService.Services;

namespace StatisticsService.Rabbit;

public class MessageHandler
{
    private readonly IClickhouseClient _clickhouse;
    private readonly IMessageConverter _converter;

    public MessageHandler(IClickhouseClient clickhouse, IMessageConverter converter)
    {
        _clickhouse = clickhouse;
        _converter = converter;
    }
    public void HandleMessage(NewShortUrlMessage message)
    {
        Console.WriteLine($"Received message with content {message.ShortUrl}");
        _clickhouse.AddEntry(_converter.ConvertNewShortLinkMessageToClickhouseEntry(message));
    }
}