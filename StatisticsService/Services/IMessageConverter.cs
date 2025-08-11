using StatisticsService.Model;
using StatisticsService.Models;

namespace StatisticsService.Services;

public interface IMessageConverter
{
    public ClickhouseEntry ConvertNewShortLinkMessageToClickhouseEntry(NewShortUrlMessage message);
}