using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Delete
{
    public static class DeletePublisher
    {
        public static async Task RemovePublisher(Publisher publisher)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Publishers.Remove(publisher);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting {publisher.PublisherID} ID: {ex.Message}");
            }
        }
    }
}
