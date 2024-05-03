using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.Services.Read.ReadCategory
{
    public static class ReadGetCategoryById
    {
        public static async Task<Category> GetCategoryById(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Categories.Include(b => b.Books).SingleAsync(c => c.CategoryID == id);

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching category '{id}': {ex.Message}");
            }
        }
    }
}
