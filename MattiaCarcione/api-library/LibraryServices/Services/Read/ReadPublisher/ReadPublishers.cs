using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadPublisher
{
    public static class ReadPublishers
    {
        public static async Task<List<Publisher>> GetAllPublishers()
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Publishers.ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving publishers: {ex.Message}");
            }
        }
    }
}
