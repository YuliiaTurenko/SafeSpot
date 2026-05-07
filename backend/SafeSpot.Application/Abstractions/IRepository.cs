namespace SafeSpot.Application.Abstractions;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}