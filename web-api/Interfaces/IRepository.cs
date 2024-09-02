

using Models.Metadata;
using System.Runtime.ExceptionServices;

/**
*TODO:
*3. Realizzare uno strato di repository per la persistenza e la lettura dei dati. I 
*repository dovranno gestire le classiche operazioni CRUD per tutte le entità. La 
*ricerca, nel caso di Autore, Categoria e Editore, può essere sostituita da un GetAll() 
*che restituisce tutti gli elementi presenti sul database.
*/
namespace Interfaces;

/// <summary>
/// Defines a contract exposing the CRUD operation of the current context.
/// </summary>
/// <typeparam name="T">The entity type of the current context.</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Adds the entity <typeparamref name="T"/> in the current context.
    /// </summary>
    /// <param name="entity">The specified entity to add.</param>
    /// <returns>
    /// An asynchronous task that represents the adding operation in the current context.
    /// </returns>
    Task AddAsync(T entity);

    /// <summary>
    /// Updates the entity <typeparamref name="T"/> in the current context.
    /// </summary>
    /// <param name="entity">The entity to update in the current context.</param>
    void Update(T entity);

    /// <summary>
    /// Remove the entity <typeparamref name="T"/> from the current context.
    /// </summary>
    /// <param name="entity">The entity to remove from the current context.</param>
    void Delete(T entity);

    /// <summary>
    /// Gets the entity <typeparamref name="T"/> with the <paramref name="id"/>, whether or not passing a lambda expression <see cref="Func{T, TResult}"/>.
    /// </summary>
    /// <param name="id">The entity id.</param>
    /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> representing a LINQ expression, such as including.
    /// <para>
    /// Defaults to <see langword="null"/>.
    /// </para>
    /// </param>
    /// <returns>A task representing asynchronous operation. The task result is the entity <typeparamref name="T"/> with the specified <paramref name="id"/>.</returns>
    /// <exception cref="Exception">If an error occurs during the execution of the task, the exception is captured and returned.</exception>
    Task<T?> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>>? queryLinq = null);

    /// <summary>
    /// Gets a list of entities of type <typeparamref name="T"/> and <see cref="PaginationMetadata"/> metadata by passing a <see cref="Func{T, TResult}"/> <paramref name="expression"/>.
    /// </summary>
    /// 
    /// <param name="pageNumber">the current of page number, starting from 1</param>
    /// <param name="pageSize">the number of the item including in each page</param>
    /// <param name="queryLinq">A <see cref="Func{T, TResult}"/> that takes a LINQ operation, such as sorting.
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
    Task<(IEnumerable<T>, PaginationMetadata)> GetAllAsync(int pageNumber, int pageSize, Func<IQueryable<T>, IQueryable<T>> queryLinq);

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// 
    /// <returns>
    /// A task operation that represents the asynchronous save operation.
    /// </returns>
    Task SaveChangesAsync();
}
