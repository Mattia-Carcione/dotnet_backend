using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using LibraryExceptions;
using LibraryServices.Services.Read.ReadBooking;
using LibraryServices.Services.Read.ReadBook;
using LibraryServices.Services.Update;

namespace LibraryServices.BookingServices
{
    public static class AddNewBooking
    {
        public static async Task NewBooking(string user, int bookID)
        {
            try
            {
                var book = await ReadGetBookById.GetBookById(bookID);

                bool isBookSoldOut = await ReadBookIsSoldOut.IsBookSoldOut(bookID);

                if (isBookSoldOut)
                {
                    throw new BookingExceptions(book.Title, BookingExceptions.BookingException.BookSoldOut);
                }

                var userBookings = await ReadBookingsByUser.SearchBookingsByUser(user);

                if (userBookings.Count >= 3)
                {
                    throw new BookingExceptions(BookingExceptions.BookingException.UserAlreadyHasThreeBooking);
                }

                bool isBookReturned = await ReadBookIsReturned.IsBookReturned(bookID, user);

                bool isUserBookings = userBookings.Any(booking => booking.Book != null && booking.Book.BookID == bookID && !isBookReturned);

                if (isUserBookings)
                {
                    throw new BookingExceptions(book.Title, BookingExceptions.BookingException.UserAlreadyBookedSameBook);
                }

                book.TotalCopiesLeft -= 1;
                await UpdateBook.EditBook(book);

                var newBooking = new Booking { User = user, BookingDate = DateTime.Now, Book = book };

                try
                {
                    using (var context = new LibraryDBContext())
                    {
                        context.Books.Attach(book);
                        await context.Bookings.AddAsync(newBooking);
                        await context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while saving booking for user '{user}' and book '{book.Title}': {ex.Message}");
                }
            }
            catch (BookingExceptions ex)
            {
                throw new Exception($"An error occurred while booking for user '{user}': {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while booking for user '{user}': {ex.Message}");
            }
        }
    }
}
