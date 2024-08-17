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

using Context;
using Exceptions;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Repository;

namespace Services;

public class BookService : GenericRepository<Book>, IBookService
{
    public BookService(LibraryContext context)
        : base(context) { }

    public async Task BookingAsync(string user, int bookId)
    {
        try
        {
            var book =
                await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId)
                ?? throw new Exception($"An error occurred: Book not found");

            if (book.Copies <= 0)
                throw new BookingException(BookingException.Exceptions.BookNotAvailable, book);

            var userBookings = await _context.Bookings.Where(b => b.User == user).ToListAsync();

            if (userBookings.Count(b => b.DeliveryDate == default) >= 3)
                throw new BookingException(BookingException.Exceptions.ToManyBookings, book);

            if (
                userBookings
                    .Where(b => b.Book != null && b.Book.Id == bookId)
                    .Any(b => b.DeliveryDate == default)
            )
                throw new BookingException(BookingException.Exceptions.ExistingBooking, book);

            var newBooking = new Booking
            {
                User = user,
                Book = book,
                BookingDate = DateTime.Now
            };

            book.Copies--;

            await _context.Bookings.AddAsync(newBooking);

            Update(book);

            await SaveChangesAsync();
        }
        catch (BookingException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task DeliveryAsync(string user, int bookingId, int bookId)
    {
        try
        {
            var booking =
                await _context.Bookings.Include(b => b.Book).FirstOrDefaultAsync(b => b.Id == bookingId)
                ?? throw new Exception($"An error occurred: Reservation not found");

            if (booking.Book == null)
                throw new Exception($"An error occurred: Book not found");

            if (booking.User != user)
                throw new Exception($"An error occurred: Reservation and User do not match");

            if (booking.DeliveryDate != default)
                throw new Exception($"An error occurred: Book has already returned");

            var book = booking.Book;

            if (book.Id != bookId)
                throw new Exception($"An error occurred: Book hasn't reservation");

            book.Copies++;

            booking.DeliveryDate = DateTime.Now;

            _context.Bookings.Update(booking);

            Update(book);

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}
