using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Delete
{
    public static class DeleteAuthor
    {
        public static async Task RemoveAuthor(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var author = await context.Authors.Where(a => a.AuthorID == id).FirstAsync();
                    context.Authors.Remove(author);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting author with {id} ID: {ex.Message}");
            }
        }
    }
}
