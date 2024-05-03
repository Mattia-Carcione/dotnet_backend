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
    public static class ReadBooksByNumbOfPages
    {
        public static async Task<List<Book>> SearchBooksByNumberOfPages(int pageOffset, int pageLimit)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.Where(book => book.NumberOfPages >= pageOffset && book.NumberOfPages <= pageLimit).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching books by range of number of pages '{pageOffset}' - '{pageLimit}': {ex.Message}");
            }
        }
    }
}
