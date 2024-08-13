namespace Interfaces;

public interface IRepository<T>
{
    void AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetAsync(int id);
    Task<List<T>> GetAllAsync(int id);
    void SaveChangesAsync();
}
