using Microsoft.AspNetCore.Mvc;

namespace ShortenerService.Controllers

{
    [ApiController]
    [Route("api/[controller]")]

    public class ShortenerController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "This is the Shortener Service!";
        }
    }
}
