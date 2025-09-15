using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ShortenerService.Controllers;

public interface IShortenerController
{
    public ActionResult<string> Hello();

    public Task<ActionResult<string>> CreateNewShortUrl(string longUrl);
}