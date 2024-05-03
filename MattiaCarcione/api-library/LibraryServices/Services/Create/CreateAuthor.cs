using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModel.Model;
using LibraryContext;

namespace LibraryServices.Services.Create
{
    public static class CreateAuthor
    {
        public static async Task AddAuthor(Author author)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    if (author.Books != null)
                    {
                        foreach (var book in author.Books)
                        {
                            context.Books.Attach(book);
                        }
                    }
                    await context.Authors.AddAsync(author);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving author '{author.FirstName}': {ex.Message}");
            }
        }
    }
}
