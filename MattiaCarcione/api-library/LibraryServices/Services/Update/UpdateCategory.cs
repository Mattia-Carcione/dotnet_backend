using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Update
{
    public static class UpdateCategory
    {
        public static async Task EditCategory(Category category)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Categories.Update(category);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { throw new Exception($"An error occurred while updating {category.CategoryID} ID: {ex.Message}"); }
        }
    }
}
