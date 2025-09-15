using ShortenerService.Models;

namespace ShortenerService.Rabbit;

public interface ISender
{
    public void Send(NewShortUrlMessage message);
}