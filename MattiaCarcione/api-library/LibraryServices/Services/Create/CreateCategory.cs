using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Create
{
    public static class CreateCategory
    {
        public static async Task AddCategory(Category category)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    if (category.Books != null)
                    {
                        foreach (var book in category.Books)
                        {
                            context.Books.Attach(book);
                        }
                    }
                    await context.Categories.AddAsync(category);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving category '{category.Genre}': {ex.Message}");
            }
        }
    }
}
