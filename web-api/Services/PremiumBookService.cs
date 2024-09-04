using Context;
using Exceptions;
using Helpers;
using Interfaces;
using LibraryTests;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System.Runtime.ExceptionServices;

namespace Services
{
    /// <summary>
    /// An instance of <see cref="PremiumBookService"/> providing the operation to buy a book.
    /// <para>
    /// This class extends <see cref="BookService"/>.
    /// </para>
    /// <para>
    /// This class implements <see cref="IPremiumServiceBook"/>.
    /// </para>
    /// </summary>
    public class PremiumBookService : BookService, IPremiumServiceBook
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PremiumBookService"/> using the specified <paramref name="context"/> of type <see cref="LibraryContext"/>.
        /// </summary>
        /// <param name="context"></param>
        public PremiumBookService(LibraryContext context) :base(context) { }

        /// <summary>
        /// Buys a specified book for a premium user.
        /// </summary>
        /// <param name="email">The email address of the user.</param>
        /// <param name="bookId">The id of the specified book to be bought.</param>
        /// <returns>
        /// A task representing asynchronous operation that returns a new instance of <see cref="Order"/>.
        /// </returns>
        /// <exception cref="BookingException">Thrown when booking-specific rules are violated.</exception>
        /// <exception cref="Exception">Thrown when an unexpected error occurs during the booking process.</exception>
        public async Task<Order> BuyBookAsync(string email, int bookId)
        {
            try
            {
                ValidatorHelper.CheckIsValid(
                    email,
                    u => !string.IsNullOrEmpty(u),
                    BookingException.Exceptions.UserFieldIsRequired
                );

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email)
                    ?? throw new BookingException(BookingException.Exceptions.UserNotFound);

                var book = await GetAsync(bookId)
                    ?? throw new BookingException(BookingException.Exceptions.BookNotFound);

                ValidatorHelper.CheckIsValid(
                        book.TotalCopies,
                        copies => copies > 0,
                        BookingException.Exceptions.BookNotAvailable
                    );

                var userBooking = await _context
                        .Bookings.FirstOrDefaultAsync(u => u.User == user && u.BookId == bookId);

                if (userBooking != null && userBooking.ReturnDate == default)
                    await UpdateBookingAsync(email, userBooking.Id, bookId);

                var newOrder = EntityFactoryHelper.CreateOrder(user.Id, bookId);

                if (book.Copies > 0)
                    book.Copies--;
                else book.Copies++;

                book.TotalCopies--;

                await _context.Orders.AddAsync(newOrder);

                await UpdateBookState(book);

                return newOrder;
            }
            catch (BookingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
                throw;
            }
        }
    }
}
