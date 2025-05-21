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
    public ActionResult<string> Get()
    {
        return "This is the Resolver Service!";
    }
    
    
    
}