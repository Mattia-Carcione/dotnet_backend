using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryContext;

namespace LibraryServices.Services.Read.ReadBook
{
    public static class ReadBookIsReturned
    {
        public static async Task<bool> IsBookReturned(int bookID, string user)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    bool boolean = await context.Bookings
                    .Where(booking => booking.Book != null && booking.Book.BookID == bookID)
                    .Where(booking => booking.User == user)
                    .AnyAsync(booking => booking.DeliveryDate != null);

                    return boolean;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if book '{bookID}' is returned: {ex.Message}");
            }
        }
    }
}
