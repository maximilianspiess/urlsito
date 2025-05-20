using Microsoft.AspNetCore.Mvc;

namespace ShortenerService.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    public class ShortenerController : ControllerBase
    {
        private Services.ShortenerService _service = new();
        
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is the Shortener Service!";
        }

        [HttpPost]
        public ActionResult<string> CreateNewShortUrl(string longUrl)
        {
            return _service.ShortenUrl(longUrl);
        }
    }
}
