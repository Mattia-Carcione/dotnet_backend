using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Update
{
    public static class UpdateAuthor
    {
        public static async Task EditAuthor(Author author)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Authors.Update(author);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating {author.AuthorID} ID: {ex.Message}");
            }
        }
    }
}
