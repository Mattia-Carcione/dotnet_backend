using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Delete
{
    public static class DeleteCategory
    {
        public static async Task RemoveCategory(Category category)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Categories.Remove(category);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting {category.CategoryID} ID: {ex.Message}");
            }
        }
    }
}
