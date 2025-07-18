using Microsoft.AspNetCore.Mvc;
using ResolverService.Services;

namespace ResolverService.Controller;

[ApiController]
[Route("api/[controller]")]
public class ResolverController
{
    private readonly LinkResolverService _service;

    public ResolverController(LinkResolverService service)
    {
        _service = service;
    }

    [HttpGet("hello")]
    public ActionResult<string> Hello()
    {
        return "It's the Resolver Service!";
    }

    [HttpGet("resolve/{shortUrl}")]
    public ActionResult<string> ResolveUrl(string shortUrl)
    {
        return _service.ResolveUrl(shortUrl);
    }
    
    
    
}