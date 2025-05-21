using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortenerService.Models;

public class ShortLink
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public ObjectId Id { get; set; }
    [BsonElement("hash")] public string HashUrl { get; set; }

    [BsonElement("createdAt")] public DateTime CreatedAt { get; } = DateTime.UtcNow;
    [BsonElement("shortUrl")] public string ShortUrl { get; set; } = null!;
}