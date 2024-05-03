using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModel.Model;
using LibraryContext;

namespace LibraryServices.Services.Create
{
    public static class CreatePublisher
    {
        public static async Task AddPublisher(Publisher publisher)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    if (publisher.Books != null)
                    {
                        foreach (var book in publisher.Books)
                        {
                            context.Books.Attach(book);
                        }
                    }
                    await context.Publishers.AddAsync(publisher);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving author '{publisher.Name}': {ex.Message}");
            }
        }
    }
}
