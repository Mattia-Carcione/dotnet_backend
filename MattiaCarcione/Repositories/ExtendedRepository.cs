using System.Linq.Expressions;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ExtendedRepository<T> : GenericRepository<T>, IExtendedRepository<T> where T : class
{
    public ExtendedRepository(LibraryContext context) : base(context) { }

    public async Task<List<T>> SearchByCriteriaAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
