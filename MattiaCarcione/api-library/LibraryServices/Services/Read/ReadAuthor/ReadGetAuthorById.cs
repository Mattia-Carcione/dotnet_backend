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
    public static class ReadGetAuthorById
    {
        public static async Task<Author> GetAuthorById(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Authors.Include(a => a.Books).SingleAsync(a => a.AuthorID == id);

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching author '{id}': {ex.Message}");
            }
        }
    }
}

