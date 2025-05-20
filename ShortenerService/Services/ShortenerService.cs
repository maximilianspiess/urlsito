namespace ShortenerService.Services;

public class ShortenerService
{
    public ShortenerService(){}
    
    public string ShortenUrl(string longUrl)
    {
        var uuid = GenerateShortUuid();
        throw new NotImplementedException();
    }

    private string GenerateShortUuid()
    {
        var uuid = Guid.NewGuid();
        // TODO need to shorten!
        return uuid.ToString();
    }
}