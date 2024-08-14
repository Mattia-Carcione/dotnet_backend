using System.Linq.Expressions;

namespace Interfaces;

public interface ISearchRepository<T>
{
    Task<List<T>> SearchByCriteria(Expression<Func<T, bool>> expression);
}
