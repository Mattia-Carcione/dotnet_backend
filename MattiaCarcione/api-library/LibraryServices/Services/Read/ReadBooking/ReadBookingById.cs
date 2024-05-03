using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Read.ReadBooking
{
    public class ReadBookingById
    {
        public static async Task<Booking> GetBookingById(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Bookings.Include(b => b.Book).SingleAsync(b => b.BookingID == id);

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while searching booking '{id}': {ex.Message}");
            }
        }
    }
}
