using Microsoft.AspNetCore.Mvc;
using StatisticsService.Model;

namespace StatisticsService.Controllers;

[ApiController]
[Route("api/[controller]")]

public class StatisticsController: ControllerBase, IStatisticsController
{
    public ActionResult<ShortLinkStatistics> getShortLinkStatistics(string shortUrl)
    {
        throw new NotImplementedException();
    }
}