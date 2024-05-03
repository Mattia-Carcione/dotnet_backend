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
    public static class DeleteBook
    {
        public static async Task RemoveBook(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var book = await context.Books.Where(a => a.BookID == id).FirstAsync();
                    context.Books.Remove(book);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting book with {id} ID: {ex.Message}");
            }
        }
    }
}
