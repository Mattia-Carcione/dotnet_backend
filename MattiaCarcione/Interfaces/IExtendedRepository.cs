using System.Linq.Expressions;

namespace Interfaces;

public interface IExtendedRepository<T>
{
    Task<List<T>> SearchByCriteria(Expression<Func<T, bool>> expression);
}
