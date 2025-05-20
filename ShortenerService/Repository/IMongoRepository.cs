namespace ShortenerService.Repository;

public interface IMongoRepository<T> where T: class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(string id);
    Task AddAsync(T entity);
}