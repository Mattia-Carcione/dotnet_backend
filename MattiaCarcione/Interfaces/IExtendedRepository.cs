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

namespace Interfaces;

public interface IExtendedRepository<T>
{
    Task<List<T>> SearchByCriteria(Expression<Func<T, bool>> expression);
}
