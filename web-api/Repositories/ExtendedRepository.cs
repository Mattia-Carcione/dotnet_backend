using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Metadatas;

namespace Repository;

public class ExtendedRepository<T> : GenericRepository<T>, IExtendedRepository<T> where T : class
{
    public ExtendedRepository(LibraryContext context) : base(context) { }

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
