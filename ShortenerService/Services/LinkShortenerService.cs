using System.Security.Cryptography;
using System.Text;
using ShortenerService.Models;
using ShortenerService.Rabbit;
using ShortenerService.Repository;

namespace ShortenerService.Services;

public class LinkShortenerService
{
    private readonly IShortLinkRepository _repository;
    private readonly ISender _sender;

    public LinkShortenerService(IShortLinkRepository repository, ISender sender)
    {
        _repository = repository;
        _sender = sender;
    }

    public async Task<string> ShortenUrl(string longUrl)
    {
        var hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(longUrl));
        var hashLongUrlString = Convert.ToHexStringLower(hashBytes);
        var hashLongUrlInt = BitConverter.ToUInt128(hashBytes);
        var shortUrl = (hashLongUrlInt % (1 << 30)).ToString("x8");

        var shortLink = new ShortLink
        {
            HashLongUrl = hashLongUrlString,
            ShortUrl = shortUrl
        };

        await _repository.AddAsync(shortLink);

        var message = new NewShortUrlMessage
        {
            longUrl = longUrl,
            shortUrl = shortUrl
        };
        
        _sender.Send(message);
        
        return shortUrl;
    }
}