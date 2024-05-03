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
    public static class ReadBooksByPublishingDate
    {
        public static async Task<List<Book>> SearchBooksByPublishingDate(DateTime dateOffset, DateTime dateLimit)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.Where(book => book.PublishingDate >= dateOffset && book.PublishingDate <= dateLimit).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching books by publishing date range '{dateOffset}' - '{dateLimit}': {ex.Message}");
            }
        }
    }
}
