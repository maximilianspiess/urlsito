using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;
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

        var existingLink = await _repository.GetByLongUrlAsync(hashLongUrlString);
        ShortLink shortLink;
        string shortUrl;

        if (existingLink != null)
        {
            shortUrl = existingLink.ShortUrl;
        }
        else
        {
            var hashLongUrlInt = BitConverter.ToUInt128(hashBytes);
            shortUrl = (hashLongUrlInt % (1 << 30)).ToString("x8");

            shortLink = new ShortLink
            {
                HashLongUrl = hashLongUrlString,
                ShortUrl = shortUrl
            };

            await _repository.AddAsync(shortLink);
        }

        var message = new NewShortUrlMessage
        {
            longUrl = longUrl,
            shortUrl = shortUrl
        };
        
        _sender.Send(message);
        
        return shortUrl;
    }
}