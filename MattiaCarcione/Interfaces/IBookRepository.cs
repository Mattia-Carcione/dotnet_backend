using Model.Entities;

namespace Interfaces;

public interface IBookRepository
{
    Task<List<Book>> SearchBooksAsync(string partialTitle, string partialAuthorLastName, List<string> categories);
}
