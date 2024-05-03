using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.Services.Read.ReadBooking
{
    public static class ReadBookings
    {
        public static async Task<List<Booking>> GetAllBookings()
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var query = await context.Bookings.Include(b => b.Book).ToListAsync();

                    return query;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving authors: {ex.Message}");
            }
        }
    }
}
