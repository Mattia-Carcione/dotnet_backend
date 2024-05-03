using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Update
{
    public static class UpdateBook
    {
        public static async Task EditBook(Book book)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Books.Update(book);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating {book.BookID} ID: {ex.Message}");
            }
        }
    }
}
