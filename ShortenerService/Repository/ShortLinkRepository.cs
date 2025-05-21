using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShortenerService.Models;
using ShortenerService.Mongo;

namespace ShortenerService.Repository;

public class ShortLinkRepository : IShortLinkRepository
{
    private readonly IMongoCollection<ShortLink> _collection;

    public ShortLinkRepository(IMongoDatabase database, IOptions<MongoDbSettings> settings)
    {
        _collection = database.GetCollection<ShortLink>(settings.Value.CollectionName);
    }

    public async Task<List<ShortLink>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<ShortLink> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<ShortLink>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    }

    public async Task AddAsync(ShortLink entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}