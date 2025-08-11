using StatisticsService.Model;
using StatisticsService.Models;

namespace StatisticsService.Services;

public class MessageConverter: IMessageConverter
{
    public ClickhouseEntry ConvertNewShortLinkMessageToClickhouseEntry(NewShortUrlMessage message)
    {
        return new ClickhouseEntry
        {
            ShortUrl = message.ShortUrl,
            LongUrl = message.LongUrl
        };
    }
}