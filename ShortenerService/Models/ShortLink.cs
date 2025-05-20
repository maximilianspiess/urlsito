using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortenerService.Models;

public class ShortLink
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? HashUrl { get; set; }

    [BsonElement("shortUrl")] public string ShortUrl { get; set; } = null!;


}