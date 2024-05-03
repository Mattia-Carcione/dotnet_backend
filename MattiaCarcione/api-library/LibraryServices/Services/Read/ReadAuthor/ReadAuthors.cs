using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadAuthor
{
    public static class ReadAuthors
    {
        public static async Task<List<Author>> GetAllAuthors()
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Authors.ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving authors: {ex.Message}");
            }
        }
    }
}
