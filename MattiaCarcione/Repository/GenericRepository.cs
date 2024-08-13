using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class GenericRepository<T> : IRepository<T>
    where T : class
{
    protected readonly LibraryContext _context;

    public GenericRepository(LibraryContext context) => _context = context;

    public async Task AddAsync(T entity)
    {
        try
        {
            await _context.AddAsync(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public void Update(T entity)
    {
        try
        {
            _context.Update(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public void Delete(T entity)
    {
        try
        {
            _context.Remove(entity);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public async Task<T> GetAsync(int id)
    {
        try
        {
            var entity = await _context.FindAsync<T>(id);
            if (entity == null)
                throw new Exception($"The entity with id: {id} not found");

            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public async Task<List<T>> GetAllAsync()
    {
        try
        {
            return await _context.Set<T>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
