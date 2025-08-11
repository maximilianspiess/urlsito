using StatisticsService.Model;
using StatisticsService.Models;

namespace StatisticsService.Clickhouse;

public interface IClickhouseClient
{
    public ClickhouseEntry GetEntryByShortUrl(string shortUrl);

    public ClickhouseEntry[] GetAllEntries();

    public void AddEntry(ClickhouseEntry entry);
}