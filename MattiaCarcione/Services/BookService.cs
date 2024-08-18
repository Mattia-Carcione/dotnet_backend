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
using Model.Entities;
using Repository;

namespace Services;

public class BookService : ExtendedRepository<Book>, IBookService
{
    public BookService(LibraryContext context)
        : base(context) { }

    private async Task UpdateBookState(Book book)
    {
        Update(book);
        await SaveChangesAsync();
    }

    public async Task BookingAsync(string user, int bookId)
    {
        {
            try
            {
                ValidatorHelper.CheckIsValid(user, u => !string.IsNullOrEmpty(u), BookingException.Exceptions.UserFieldIsRequired);

                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId) ?? throw new BookingException(BookingException.Exceptions.BookNotFound);;

                var userBookings = await _context.Bookings.Where(b => b.User == user && b.DeliveryDate == default).ToListAsync();

                ValidatorHelper.CheckIsValid(userBookings, u => !u.Any(b => b.Book != null && b.Book.Id == bookId), BookingException.Exceptions.ExistingBooking);

                ValidatorHelper.CheckIsValid(userBookings, e => userBookings.Count < 3, BookingException.Exceptions.ToManyBookings);

                ValidatorHelper.CheckIsValid(book, e => book.Copies > 0, BookingException.Exceptions.BookNotAvailable);

                var newBooking = EntityFactoryHelper.CreateBooking(user, book);

                book.Copies--;

                await _context.Bookings.AddAsync(newBooking);

                await UpdateBookState(book);
            }
            catch (BookingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }

        }
    }

    public async Task DeliveryAsync(string user, int bookingId, int bookId)
    {
        {
            try
            {
                ValidatorHelper.CheckIsValid(user, u => !string.IsNullOrEmpty(u), BookingException.Exceptions.UserFieldIsRequired);

                var booking =
                    await _context.Bookings.Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == bookingId && b.User == user)
                    ?? throw new BookingException(BookingException.Exceptions.UserMismatch);

                ValidatorHelper.CheckIsValid(booking.DeliveryDate, d => d == default, BookingException.Exceptions.BookAlreadyReturned);

                var book = booking.Book!;

                ValidatorHelper.CheckIsValid(book.Id, b => b == bookId, BookingException.Exceptions.BookMismatch);

                book.Copies++;

                booking.DeliveryDate = DateTime.Today;

                _context.Bookings.Update(booking);

                await UpdateBookState(book);
            }
            catch (BookingException)
            {
                throw;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
        }
    }
}
