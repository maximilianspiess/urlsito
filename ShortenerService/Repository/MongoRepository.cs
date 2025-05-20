using MongoDB.Bson;
using MongoDB.Driver;

namespace ShortenerService.Repository;

public class MongoRepository<T>: IMongoRepository<T> where T: class
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        return await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}