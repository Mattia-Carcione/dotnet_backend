using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class GenericRepository<T> : IRepository<T>
    where T : class
{
    private readonly LibraryContext _context;

    public GenericRepository(LibraryContext context)
    {
        _context = context;
    }

    public async void AddAsync(T entity)
    {
        await _context.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _context.Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public async Task<T> GetAsync(int id)
    {
        var entity = await _context.FindAsync<T>(id);

        if (entity == null)
            throw new Exception($"An error occurred: The entity with id: {id} not found");

        return entity;
    }

    public async Task<List<T>> GetAllAsync(int id)
    {
        return await _context.Set<T>().ToListAsync();
    }

    public void SaveChangesAsync()
    {
        _context.SaveChangesAsync();
    }
}
