using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Update
{
    public static class UpdatePublisher
    {
        public static async Task EditPublisher(Publisher publisher)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Publishers.Update(publisher);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex) { throw new Exception($"An error occurred while updating {publisher.PublisherID} ID: {ex.Message}"); }
        }
    }
}
