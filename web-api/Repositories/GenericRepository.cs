using System.Runtime.ExceptionServices;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Repository;

/// <summary>
/// An instance of <see cref="GenericRepository{T, TContext}"/> that includs the CRUD operation of the context.
/// <para>
/// This class extends <see cref="IRepository{T}"/>
/// </para>
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
/// <typeparam name="TContext">The type of the current context.</typeparam>
public class GenericRepository<T, TContext> : IRepository<T>
    where T : class where TContext : DbContext
{
    /// <summary>
    /// Provides access to database related information and operations for <typeparamref name="TContext"/>.
    /// </summary>
    protected readonly TContext _context;

    /// <summary>
    /// Initializes a new instance of <see cref="GenericRepository{T}"/> using the specified <paramref name="context"/> of type <typeparamref name="TContext"/>.
    /// </summary>
    /// <param name="context">The current context.</param>
    public GenericRepository(TContext context) => _context = context;

    /// <summary>
    /// Adds the entity <typeparamref name="T"/> in the current context.
    /// </summary>
    /// 
    /// <param name="entity">The specified entity to add.</param>
    /// 
    /// <returns>
    /// An asynchronous task that represents the adding operation in the current context.
    /// </returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
    /// Updates the entity <typeparamref name="T"/> in the current context.
    /// </summary>
    /// 
    /// <param name="entity">The entity to update in the current context.</param>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
    /// Remove the entity <typeparamref name="T"/> from the current context.
    /// </summary>
    /// 
    /// <param name="entity">The entity to remove from the current context.</param>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
    /// Gets the entity <typeparamref name="T"/> with the <paramref name="id"/>, whether or not passing a lambda expression <see cref="Func{T, TResult}"/>.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes a LINQ expression, such as sorting.
    /// <para>
    /// Defaults to <see langword="null"/>.
    /// </para>
    /// </param>
    /// <returns>A task representing asynchronous operation. The task result is the entity <typeparamref name="T"/> with the specified <paramref name="id"/>.</returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
    /// Gets a list of entities of type <typeparamref name="T"/> and <see cref="PaginationMetadata"/> metadata by passing a <see cref="Func{T, TResult}"/> <paramref name="expression"/>.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entity</typeparam>
    /// <param name="pageNumber">the current of page number, starting from 1</param>
    /// <param name="pageSize">the number of the item including in each page</param>
    /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes a LINQ operation. 
    /// <para>
    /// Sorting must be provided.
    /// </para>
    /// </param>
    /// 
    /// <returns>
    /// An asynchronous task operation returning a tuple:
    /// <list type="bullet">
    /// 
    /// <item>
    /// <description><see cref="IEnumerable{T}"/> represents the entities that match the criteria</description>
    /// </item>
    /// 
    /// <item>
    /// <description><see cref="PaginationMetadata"/> represents an object that includes pagination details</description>
    /// </item>
    /// 
    /// </list>
    /// </returns>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// 
    /// <returns>
    /// A task operation that represents the asynchronous save operation.
    /// </returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
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
