using ShortenerService.Models;

namespace ShortenerService.Repository;

public interface IShortLinkRepository
{
    Task CreateAsync(ShortLink link);
}