using ShortenerService.Models;

namespace ShortenerService.Repository;

public class ShortLinkRepository : IShortLinkRepository
{
    public Task CreateAsync(ShortLink link)
    {
        throw new NotImplementedException();
    }
}