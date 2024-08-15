using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using Repository;

namespace Services;

public class BookingService : ExtendedRepository<Booking>, IBookingService
{
    public BookingService(LibraryContext context)
        : base(context) { }

    public new void Delete(Booking booking)
    {
        throw new NotImplementedException("Delete is not supported");
    }

    public async Task BookingAsync(string user, int bookId)
    {
        try
        {
            var book =
                await _context.Books.FirstOrDefaultAsync(b => b.ID == bookId)
                ?? throw new Exception($"An error occurred");

            if (book.Copies <= 0)
                throw new Exception($"An error occurred");

            var userBookings = await SearchByCriteria(b => b.User == user);

            if (userBookings.Count >= 3)
                throw new Exception($"An error occurred");

            if (
                userBookings
                    .Where(b => b.Book != null && b.Book.ID == bookId)
                    .Any(b => b.DeliveryDate == default)
            )
                throw new Exception($"An error occurred");

            var newBooking = new Booking { User = user, Book = book };

            book.Copies = book.Copies-- < 0 ? 0 : book.Copies--;

            await AddAsync(newBooking);

            _context.Books.Update(book);

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
                await _context.Bookings.FirstOrDefaultAsync(b => b.ID == bookingId)
                ?? throw new Exception($"An error occured");

            var book = booking.Book ?? throw new Exception($"An error occurred");

            book.Copies++;

            booking.DeliveryDate = DateTime.Now;

            Update(booking);

            _context.Books.Update(book);

            await SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
