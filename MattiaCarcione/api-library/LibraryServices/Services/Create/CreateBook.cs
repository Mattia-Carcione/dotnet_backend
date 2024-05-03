using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Create
{
    public static class CreateBook
    {
        public static async Task AddBook(Book book)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var author = await context.Authors.SingleOrDefaultAsync(a => a.AuthorID == book.AuthorID);

                    if (author != null)
                    {
                        context.Authors.Attach(author);
                    }

                    if (book.Categories != null)
                    {
                        foreach (var category in book.Categories)
                        {
                            context.Categories.Attach(category);
                        }
                    }

                    if (book.Bookings != null)
                    {
                        foreach (var booking in book.Bookings)
                        {
                            context.Bookings.Attach(booking);
                        }
                    }

                    var publisher = await context.Publishers.SingleOrDefaultAsync(p => p.PublisherID == book.PublisherID);

                    if (publisher != null)
                    {
                        context.Publishers.Attach(publisher);
                    }

                    await context.Books.AddAsync(book);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving book '{book.Title}': {ex.Message}");
            }
        }
    }
}
