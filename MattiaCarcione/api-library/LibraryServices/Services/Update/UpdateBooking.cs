using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Update
{
    public static class UpdateBooking
    {
        public static async Task EditBooking(Booking booking)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    context.Bookings.Update(booking);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating {booking.BookingID} ID: {ex.Message}");
            }
        }
    }
}
