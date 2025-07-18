using Microsoft.AspNetCore.Mvc;
using ShortenerService.Services;

namespace ShortenerService.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ShortenerController : ControllerBase
{
    private readonly LinkShortenerService _service;

    public ShortenerController(LinkShortenerService service)
    {
        _service = service;
    }

    [HttpGet("hello")]
    public ActionResult<string> Hello()
    {
        return "It's the Shortener Service!";
    }

    [HttpPost("shorten")]
    public async Task<ActionResult<string>> CreateNewShortUrl([FromBody] string longUrl)
    {
        return await _service.ShortenUrl(longUrl);
    }
}