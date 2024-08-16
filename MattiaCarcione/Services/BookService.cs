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

public class BookService : ExtendedRepository<Book>, IBookService
{
    public BookService(LibraryContext context)
        : base(context) { }

    public async Task BookingAsync(string user, int bookId)
    {
        try
        {
            var book =
                await _context.Books.FirstOrDefaultAsync(b => b.ID == bookId)
                ?? throw new Exception($"Book not found");

            if (book.Copies <= 0)
                throw new BookingException(BookingException.Exceptions.BookNotAvailable, book);

            var userBookings = await _context.Bookings.Where(b => b.User == user).ToListAsync();

            if (userBookings.Count >= 3)
                throw new BookingException(BookingException.Exceptions.ToManyBookings, book);

            if (
                userBookings
                    .Where(b => b.Book != null && b.Book.ID == bookId)
                    .Any(b => b.DeliveryDate == default)
            )
                throw new BookingException(BookingException.Exceptions.ExistingBooking, book);

            var newBooking = new Booking { User = user, Book = book };

            book.Copies--;

            await _context.Bookings.AddAsync(newBooking);

            Update(book);

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }

    public async Task DeliveryAsync(int bookingId)
    {
        try
        {
            var booking =
                await _context.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId)
                ?? throw new Exception($"Booking not found");

            var book = booking.Book ?? throw new Exception($"Book not found");

            book.Copies++;

            booking.DeliveryDate = DateTime.Now;

            _context.Bookings.Update(booking);

            Update(book);

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
