namespace ShortenerService.Mongo;

public class MongoDbSettings
{
    public required string DatabaseName { get; init; }
    public required string ConnectionString { get; init; }
    
    public required string CollectionName { get; init; }
}