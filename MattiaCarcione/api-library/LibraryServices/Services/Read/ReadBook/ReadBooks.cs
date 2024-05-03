using LibraryContext;
using Microsoft.EntityFrameworkCore;
using LibraryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBooks
    {
        public static async Task<List<Book>> GetAllBooks()
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Books.ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving books: {ex.Message}");
            }
        }
    }
}
