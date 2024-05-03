using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBookIsSoldOut
    {
        public static async Task<bool> IsBookSoldOut(int bookID)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    return await context.Books
                    .Where(book => book.BookID == bookID)
                    .AnyAsync(book => book.TotalCopiesLeft == 0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if book '{bookID}' is sold out: {ex.Message}");
            }
        }
    }
}
