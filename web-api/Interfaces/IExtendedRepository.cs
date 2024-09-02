/**
*TODO:
*Nel caso del Libro la ricerca deve prevedere i seguenti criteri:
*Titolo (ricerca parziale)
*Range di date di pubblicazione (Da – a).
*Range di numero di pagine (Da – a)
*Esaurito (NumeroCopieRimaste = 0)
*Autore (ricerca parziale sul cognome dell’autore)
*Lista di categorie associate (almeno una di quella indicate in input)
*
*Nel caso della Prenotazione la ricerca deve prevedere i seguenti criteri:
*Utente che ha prenotato
*Libro prenotato
*Se il libro è stato restituito (la data di restituzione deve essere valorizzata)
*/

using System.Linq.Expressions;
using Models.Metadata;

namespace Interfaces;

/// <summary>
/// Defines a contract extending the <see cref="IRepository{T}"/> and exposing the operation method to search an entity <typeparamref name="T"/> 
/// by applying a lambda expression <see cref="Expression{TDelegate}"/> and additional <see cref="IQueryable{T}"/> LINQ operations.
/// </summary>
/// <typeparam name="T">The entity of the current context.</typeparam>
public interface IExtendedRepository<T> : IRepository<T> where T : class
{
    /// <summary>
    /// Searches for entities of type <typeparamref name="T"/> that match the specified filter <paramref name="expression"/> and applies additional LINQ operations.
    /// </summary>
    /// <param name="pageNumber">The current page number, starting from 1.</param>
    /// <param name="pageSize">The number of the item including in each page.</param>
    /// <param name="expression">A <see cref="Expression{TDelegate}"/> taht takes a lambda expression representing the filter criteria to apply.</param>
    /// <param name="queryLinq">
    /// A <see cref="Func{T, TResult}"/> that takes a LINQ operation.
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
    Task<(IEnumerable<T>, PaginationMetadata)> SearchByCriteriaAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> queryLinq);
}
