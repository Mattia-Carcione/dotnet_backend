using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;
using LibraryModel.Model;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBookByBooking
    {
        public static async Task<Book> SearchBookByBooking(int bookingID)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var book = await context.Books.Where(booking => booking.Bookings != null && booking.Bookings.Any(booking => booking.BookingID == bookingID)).FirstAsync();

                    return book;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching book by booking '{bookingID}' ID: {ex.Message}");
            }
        }
    }
}
