using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBooksByAuthor
    {
        public static async Task<List<Book>> SearchBooksByAuthor(string lastName)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.Where(book => book.Author != null && book.Author.LastName.Contains(lastName)).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching books by author '{lastName}': {ex.Message}");
            }
        }
    }
}
