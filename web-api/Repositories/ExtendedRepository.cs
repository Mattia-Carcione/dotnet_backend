using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Metadata;

namespace Repository;

/// <summary>
/// An instance of <see cref="ExtendedRepository{T, TContext}"/> that provides the methods to search item in the current context. 
/// <para>
/// This class extends <see cref="GenericRepository{T, TContext}"/>.
/// <para>
/// This class implements <see cref="IExtendedRepository{T}"/>.
/// </para>
/// </para>
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
/// <typeparam name="TContext">The type of the current context.</typeparam>
public class ExtendedRepository<T, TContext> : GenericRepository<T, TContext>, IExtendedRepository<T> where T : class where TContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of <see cref="ExtendedRepository{T}"/> using the specified <paramref name="context"/> of type <typeparamref name="TContext"/>.
    /// </summary>
    /// <param name="context">The type of the context.</param>
    public ExtendedRepository(TContext context) : base(context) { }

    /// <summary>
    /// Gets a list of entities of type <typeparamref name="T"/> and <see cref="PaginationMetadata"/> metadata by passing a <see cref="Expression{TDelegate}"/> <paramref name="expression"/>
    /// and additional <see cref="IQueryable{T}"/> LINQ operations.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entity being required</typeparam>
    /// <param name="pageNumber">The current page number, starting from 1.</param>
    /// <param name="pageSize">The number of the item including in each page.</param>
    /// <param name="expression">A <see cref="Expression{TDelegate}"/> taht takes a lambda expression representing the filter criteria to apply.</param>
    /// <param name="queryLinq">
    /// A <see cref="IQueryable{T}"/> function that takes a LINQ operation.
    /// <para>
    /// Sorting must be provided.
    /// </para>
    /// </param>
    /// 
    /// <returns>
    /// <list type="bullet">
    /// 
    /// <item>
    /// <description><see cref="IEnumerable{T}"/> representing the entities that match the criteria.</description>
    /// </item>
    /// 
    /// <item>
    /// <description><see cref="PaginationMetadata"/> representing an object that includes pagination details.</description>
    /// </item>
    /// 
    /// </list>
    /// </returns>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
    public async Task<(IEnumerable<T>, PaginationMetadata)> SearchByCriteriaAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> queryLinq)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>().Where(expression);

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
    /// Gets the entity by passing a specified <see cref="Func{T, TResult}"/> of <see cref="IQueryable{T}"/>.
    /// </summary>
    /// 
    /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes LINQ operation.</param>
    /// 
    /// <returns>
    /// A task representing asynchronous operation that returns the entity <typeparamref name="T"/>.
    /// </returns>
    /// 
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned</exception>
    public async Task<T?> SearchEntityByCriteriaAsync(Func<IQueryable<T>, IQueryable<T>> queryLinq)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            query = queryLinq(query);

            return await query.FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }
}
