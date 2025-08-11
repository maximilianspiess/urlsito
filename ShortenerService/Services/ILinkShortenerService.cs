namespace ShortenerService.Services;

public interface ILinkShortenerService
{
    public Task<string> ShortenUrl(string longUrl);

}