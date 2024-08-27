using System.Runtime.ExceptionServices;
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
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
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
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
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
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }

    public async Task<T?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if(include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>>? include = null)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if(include != null)
                query = include(query);

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
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
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }
}
