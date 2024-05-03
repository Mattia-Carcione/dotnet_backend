using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;

namespace LibraryServices.Services.Read.ReadBooking
{
    public static class ReadUserByBooking
    {
        public static async Task<string> SearchUserByBooking(int bookingID)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var booking = await context.Bookings
                    .Where(reservation => reservation.BookingID == bookingID)
                    .FirstAsync();

                    return booking.User;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching user by booking '{bookingID}' ID: {ex.Message}");
            }
        }
    }
}
