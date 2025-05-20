using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ShortenerService.Models;

public interface IShortLink
{
    [BsonId] [BsonRepresentation(BsonType.String)]
    string? HashUrl { get; set; }
    
    DateTime CreatedAt { get; }
}