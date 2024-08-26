using System.Linq.Expressions;
using System.Runtime.ExceptionServices;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExtendedRepository<T> : GenericRepository<T>, IExtendedRepository<T> where T : class
{
    public ExtendedRepository(LibraryContext context) : base(context) { }

    public async Task<IEnumerable<T>> SearchByCriteriaAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
            throw;
        }
    }
}
