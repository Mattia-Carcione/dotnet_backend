using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Metadatas;

namespace Repository;

public class ExtendedRepository<T> : GenericRepository<T>, IExtendedRepository<T> where T : class
{
    public ExtendedRepository(LibraryContext context) : base(context) { }

    /// <summary>
    /// Get a list of entities of type <typeparamref name="T"/> by passing a criteia <paramref name="expression"/>
    /// and pagination metadata 
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the entities being required</typeparam>
    /// <param name="pageNumber">the current of page number, starting from 1</param>
    /// <param name="pageSize">the number of the item including in each page</param>
    /// <param name="expression">A lambda expression rappresenting the filter criteria to apply <see cref="Func{T, TResult}"/></param>
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
}
