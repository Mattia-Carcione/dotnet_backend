using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadCategory
{
    public static class ReadCategories
    {
        public static async Task<List<Category>> GetAllCategory()
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Categories.ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving categories: {ex.Message}");
            }
        }
    }
}
