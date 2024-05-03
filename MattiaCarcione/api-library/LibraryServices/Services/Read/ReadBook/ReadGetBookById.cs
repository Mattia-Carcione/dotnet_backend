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
    public static class ReadGetBookById
    {
        public static async Task<Book> GetBookById(int bookID)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.Include(a => a.Author).Include(p => p.Publisher).Include(c => c.Categories).SingleAsync(b => b.BookID == bookID);

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching book by ID '{bookID}': {ex.Message}");
            }
        }
    }
}
