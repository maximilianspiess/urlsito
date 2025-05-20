using MongoDB.Bson.Serialization.Attributes;

namespace ShortenerService.Models;

public class ShortLink : IShortLink
{
    public string? HashUrl { get; set; }

    private DateTime CreatedAt = DateTime.Now;

    [BsonElement("shortUrl")]
    public string ShortUrl { get; set; } = null!;
}