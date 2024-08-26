/**
*TODO:
*3. Realizzare uno strato di repository per la persistenza e la lettura dei dati. I 
*repository dovranno gestire le classiche operazioni CRUD per tutte le entità. La 
*ricerca, nel caso di Autore, Categoria e Editore, può essere sostituita da un GetAll() 
*che restituisce tutti gli elementi presenti sul database.
*/

namespace Interfaces;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null);
    Task<IEnumerable<T>> GetAllAsync();
    Task SaveChangesAsync();
}
