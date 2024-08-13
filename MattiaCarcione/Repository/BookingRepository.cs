using Context;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Model.Entities;

namespace Repository;

public class BookingRepository(LibraryContext context) : GenericRepository<Booking>(context), IBookingRepository
{
    public async Task<List<Booking>> SearchBookingsAsync(string user, Book book, DateTime deliveryDate = default)
    {
        try
        {
            if (user == null || book == null)
                throw new Exception($"The field 'User' and 'Book' cannot be empty");

            var bookings = await _context.Bookings.Where(b => b.User == user)
            .Where(b => b.Books != null && b.Books.Contains(book))
            .Where(b => b.DeliveryDate == deliveryDate)
            .ToListAsync();

            return bookings;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred: {ex.Message}");
        }
    }
}
