using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBooksByTitle
    {
        public static async Task<List<Book>> SearchBooksByTitle(string title)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.Where(book => book.Title.Contains(title)).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while searching for book '{title}': {ex.Message}");
            }
        }
    }
}
