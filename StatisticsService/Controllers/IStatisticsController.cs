using Microsoft.AspNetCore.Mvc;
using StatisticsService.Model;

namespace StatisticsService.Controllers;

public interface IStatisticsController
{
    public ActionResult<ShortLinkStatistics> getShortLinkStatistics(string shortUrl);
}