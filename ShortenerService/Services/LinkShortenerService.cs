using System.Security.Cryptography;
using System.Text;
using ShortenerService.Models;
using ShortenerService.Repository;

namespace ShortenerService.Services;

public class LinkShortenerService
{
    private readonly IShortLinkRepository _repository;

    public LinkShortenerService(IShortLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> ShortenUrl(string longUrl)
    {
        var shortUrl = GenerateShortUuid();
        var hashLongUrl = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(longUrl));

        var shortLink = new ShortLink
        {
            HashUrl = hashLongUrl,
            ShortUrl = shortUrl
        };

        await _repository.AddAsync(shortLink);
        return shortUrl;
    }

    private string GenerateShortUuid()
    {
        var uuid = Guid.NewGuid();
        // TODO need to shorten!
        return uuid.ToString();
    }
}