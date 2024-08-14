using System.Linq.Expressions;
using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class SearchRepository<T>(LibraryContext context) : GenericRepository<T>(context), ISearchRepository<T> where T : class
{
    public async Task<List<T>> SearchByCriteria(Expression<Func<T, bool>> expression)
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
