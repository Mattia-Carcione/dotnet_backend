using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadBooking
{
    public static class ReadBookingsByUser
    {
        public static async Task<List<Booking>> SearchBookingsByUser(string user)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Bookings.Where(booking => booking.User == user).Include(b => b.Book).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching bookings by user '{user}': {ex.Message}");
            }
        }
    }
}
