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
    public static class ReadBooksByCategory
    {
        public static async Task<List<Book>> SearchBooksByCategories(string categoryName)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var category = await context.Categories.Where(c => c.Genre == categoryName).FirstOrDefaultAsync();

                    var query = await context.Books.Where(c => c.Categories.Any(c => c == category)).ToListAsync();
                    
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred searching books by category '{categoryName}': {ex.Message}");
            }
        }
    }
}
