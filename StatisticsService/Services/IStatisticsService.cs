using StatisticsService.Model;

namespace StatisticsService.Services;

public interface IStatisticsService
{
    public ShortLinkStatistics getShortLinkStatistics(string shortUrl);
}