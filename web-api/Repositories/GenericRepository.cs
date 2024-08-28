using System.Runtime.ExceptionServices;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Metadatas;

namespace Repository;

public class GenericRepository<T> : IRepository<T>
    where T : class
{
    protected readonly LibraryContext _context;

    public GenericRepository(LibraryContext context) => _context = context;

    /// <summary>
    /// Add an entity <typeparamref name="T"/> into the db
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="entity">the entity to add into the db</param>
    /// 
    /// <returns>
    /// An asynchronous task operation adding the entity into the db
    /// </returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
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

    /// <summary>
    /// Update an entity <typeparamref name="T"/> that is already into the db
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="entity">the entity to update into the db</param>
    /// 
    /// <returns>
    /// A task operation updating the entity into the db
    /// </returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
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

    /// <summary>
    /// Remove an entity <typeparamref name="T"/> from the db
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="entity">the entity to remove from the db</param>
    /// 
    /// <returns>
    /// A task operation removing the entity from the db
    /// </returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
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

    /// <summary>
    /// Get an entity <typeparamref name="T"/> by id, whether or not passing a criteria <paramref name="queryLinq">
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="id">the entity id</param>
    /// <param name="queryLinq">a function <see cref="IQueryable{T}"/> that takes a LINQ operation, such as including</param>
    /// 
    /// <returns>
    /// An asynchronous task returning an entity <typeparamref name="T"/> 
    /// </returns>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
    public async Task<T?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>>? queryLinq = null)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if(queryLinq != null)
                query = queryLinq(query);

            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }

    /// <summary>
    /// Get a list of entities of type <typeparamref name="T"/> by passing a criteia <paramref name="expression"/>
    /// and pagination metadata
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="pageNumber">the current of page number, starting from 1</param>
    /// <param name="pageSize">the number of the item including in each page</param>
    /// <param name="queryLinq">A function <see cref="IQueryable{T}"/> that takes a LINQ operation (sorting must be provided)</param>
    /// 
    /// <returns>
    /// An asynchronous task operation returning a tuple:
    /// <list type="bullet">
    /// 
    /// <item>
    /// <description>a <see cref="IEnumerable{T}"/> rappresenting the entities that match the criteria</description>
    /// </item>
    /// 
    /// <item>
    /// <description>a <see cref="PaginationMetadata"/> rappresenting an object that includes pagination details</description>
    /// </item>
    /// 
    /// </list>
    /// </returns>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
    public async Task<(IEnumerable<T>, PaginationMetadata)> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> queryLinq)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            query = queryLinq(query);

            var totalItemCount = await query.CountAsync();

            PaginationMetadata paginationMetadata = new(pageSize, pageNumber, totalItemCount);

            var collection = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

            return (collection, paginationMetadata);
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }

    /// <summary>
    /// Saves all changes to the db
    /// </summary>
    /// 
    /// <returns>
    /// An asynchronous task operation saving the changes into the db
    /// </returns>
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
