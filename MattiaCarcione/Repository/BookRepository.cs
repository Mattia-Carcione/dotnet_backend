using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Repository;

public class BookRepository : GenericRepository<Book>, IBookRepository
{
    private readonly LibraryContext _context;

    public BookRepository(LibraryContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<List<Book>> SearchBooksAsync(string partialTitle, string partialAuthorLastName, List<string> categories)
    {
        try
        {
            if (partialTitle == null || partialAuthorLastName == null || categories.Count <= 0)
                throw new Exception($"The field 'Title', 'Author Last Name' and 'Categories' cannot be empty");

            var books = await _context.Books.Where(b =>
                b.Author != null && b.Author.LastName.Contains(partialAuthorLastName))
                .Where(b => b.Title.Contains(partialTitle))
                .Where(b => b.Categories != null && b.Categories.Any(c => categories.Contains(c.Genre)))
                .ToListAsync();

            return books;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
