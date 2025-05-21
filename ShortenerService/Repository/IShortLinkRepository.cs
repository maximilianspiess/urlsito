using ShortenerService.Models;

namespace ShortenerService.Repository;

public interface IShortLinkRepository
{
    Task<List<ShortLink>> GetAllAsync();
    Task<ShortLink> GetByIdAsync(string id);
    Task AddAsync(ShortLink entity);
}