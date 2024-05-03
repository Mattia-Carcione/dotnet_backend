using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.Services.Read.ReadPublisher
{
    public static class ReadGetPublisherById
    {
        public static async Task<Publisher> GetPublisherById(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Publishers.Include(b => b.Books).SingleAsync(c => c.PublisherID == id);

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching publisher '{id}': {ex.Message}");
            }
        }
    }
}
