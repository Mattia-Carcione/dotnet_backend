//TODO:
//La logica di business sulla prenotazione consiste nel soddisfare
// contemporaneamente i seguenti vincoli:
// - Il libro deve essere disponilbe (il numero di copie disponibili deve essere
// maggiore di zero).
// - Un utente non può prenotare lo stesso libro più di una volta a meno di non
// averlo restituito.
// - Un utente non può prenotare un libro se ha già tre libri da consegnare.
// Per consegnare un libro è necessario che esista una prenotazione a carico
// dell’utente su quel libro che sia ancora aperta (data di restituzione non valorizzata).
// I servizi che non soddisfano le condizioni di cui sopra devono lanciare un’eccezione
// custom di tipo PrenotazioneException contenente un identificativo (enum)
// dell’errore riscontrato e il libro su cui si sta lavorando

using System.Runtime.ExceptionServices;
using Context;
using Exceptions;
using Helpers;
using Interfaces;
using LibraryTests;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Repository;

namespace Services;

/// <summary>
/// An instance of <see cref="BookService"/> providing the booking-related operation, including booking and updating booking.
/// <para>
/// This class extends <see cref="ExtendedRepository{T}"/>.
/// </para>
/// <para>
/// This class implements <see cref="IBookService"/>.
/// </para>
/// </summary>
public class BookService : ExtendedRepository<Book, LibraryContext>, IBookService
{
    protected static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    /// <summary>
    /// Initializes a new instance of <see cref="BookService"/> using the specified <paramref name="context"/> of type <see cref="LibraryContext"/>.
    /// </summary>
    /// <param name="context">The database context of type <see cref="LibraryContext"/> used to access the library data.</param>
    public BookService(LibraryContext context)
        : base(context) { }

    /// <summary>
    /// Updates and saves the state of the <see cref="Book"/> in the current context.
    /// </summary>
    /// <param name="book">The <see cref="Book"/> to be updated in the context.</param>
    /// <returns>
    /// A task representing the asynchronous operation to update and save the <see cref="Book"/> in the current context.
    /// </returns>
    protected async Task UpdateBookState(Book book)
    {
        Update(book);
        await SaveChangesAsync();
    }

    /// <summary>
    /// Validates booking rules for a specified user and book.
    /// </summary>
    /// <param name="userBookings">A <see cref="List{T}"/> of <see cref="Booking"/> objects associated with the user who is making the booking.</param>
    /// <param name="bookId">The id of the <see cref="Book"/> related to the booking.</param>
    private static void ValidateBookingRules(List<Booking> userBookings, int bookId)
    {
        ValidatorHelper.CheckIsValid(
            userBookings,
            u => !u.Any(b => b.Book != null && b.Book.Id == bookId),
            BookingException.Exceptions.ExistingBooking
        );

        ValidatorHelper.CheckIsValid(
            userBookings.Count,
            count => count < 3,
            BookingException.Exceptions.ToManyBookings
        );
    }

    /// <summary>
    /// Books a specified book for a user.
    /// </summary>
    /// <param name="user">The name of the user who is making the book.</param>
    /// <param name="bookId">The id of the book to be booked.</param>
    /// <returns>
    /// A <see cref="Booking"/> representing the entity that was added in the context.
    /// </returns>
    /// <exception cref="BookingException">Thrown when booking-specific rules are violated.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during the booking process.</exception>
    public async Task<Booking> BookingAsync(string email, int bookId)
    {
        await _semaphore.WaitAsync();

        try
        {
            ValidatorHelper.CheckIsValid(
                email,
                u => !string.IsNullOrEmpty(u) && u.Count(c => c == '@') == 1,
                BookingException.Exceptions.ValidEmailAddressIsRequired
            );

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                var index = email.IndexOf("@");

                var username = email.Substring(0, index);

                user = EntityFactoryHelper.CreateUser(username, email, false);

                await _context.AddAsync(user);
            }

            var book = await GetAsync(bookId)
            ?? throw new BookingException(BookingException.Exceptions.BookNotFound);

                
            ValidatorHelper.CheckIsValid(
                book.Copies,
                copies => copies > 0,
                BookingException.Exceptions.BookNotAvailable
            );

            var userBookings = await _context
                .Bookings.Where(b => b.User == user && b.ReturnDate == default)
                .ToListAsync();

            ValidateBookingRules(userBookings, bookId);

            var newBooking = EntityFactoryHelper.CreateBooking(user, book);

            book.Copies--;

            await _context.Bookings.AddAsync(newBooking);

            await UpdateBookState(book);

            return await _context.Bookings.Where(b => b.User == newBooking.User && b.Book == book && b.ReturnDate == default).FirstAsync() ?? throw new InvalidOperationException();
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
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Updates the return date of the specified <see cref="Booking"/>.
    /// </summary>
    /// <param name="email">The email of the user who is returning the book.</param>
    /// <param name="bookingId">The id of the booking to be updated.</param>
    /// <param name="bookId">The id of the book to be returned.</param>
    /// <returns>
    /// A task representing the asynchronous operation to update and save the <see cref="Booking"/> in the current context.
    /// </returns>
    /// <exception cref="BookingException">Thrown when booking-specific rules are violated.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the booking's associated book is null.</exception>
    /// <exception cref="Exception">Thrown when an unexpected error occurs during the booking process.</exception>
    public async Task UpdateBookingAsync(string email, int bookingId, int bookId)
    {
        await _semaphore.WaitAsync();

        try
        {
            ValidatorHelper.CheckIsValid(
                email,
                u => !string.IsNullOrEmpty(u) && u.Count(c => c == '@') == 1,
                BookingException.Exceptions.ValidEmailAddressIsRequired
            );

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new BookingException(BookingException.Exceptions.UserNotFound);

            var booking =
                await _context
                    .Bookings.Include(b => b.Book)
                    .FirstOrDefaultAsync(b => b.Id == bookingId)
                ?? throw new BookingException(BookingException.Exceptions.BookingNotFound);

            if(booking.User != user)
                throw new BookingException(BookingException.Exceptions.UserMismatch);

            ValidatorHelper.CheckIsValid(
                booking.ReturnDate,
                d => d == default,
                BookingException.Exceptions.BookAlreadyReturned
            );

            var book = booking.Book ?? throw new ArgumentNullException();

            ValidatorHelper.CheckIsValid(
                book.Id,
                b => b == bookId,
                BookingException.Exceptions.BookMismatch
            );

            book.Copies++;

            booking.ReturnDate = DateTime.Now;

            _context.Bookings.Update(booking);

            await UpdateBookState(book);
        }
        catch (BookingException)
        {
            throw;
        }
        catch (ArgumentNullException)
        {
            throw;
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(ex).Throw();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
