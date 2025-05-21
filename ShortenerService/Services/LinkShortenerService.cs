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
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(longUrl));
        var hashLongUrlString = Convert.ToHexStringLower(hashBytes);
        var hashLongUrlInt = BitConverter.ToUInt128(hashBytes);
        var shortUrl = (hashLongUrlInt % (1 << 30)).ToString("x8");

        var shortLink = new ShortLink
        {
            HashUrl = hashLongUrlString,
            ShortUrl = shortUrl
        };

        await _repository.AddAsync(shortLink);
        return shortUrl;
    }
}