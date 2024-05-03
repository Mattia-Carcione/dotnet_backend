using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModel.Model;
using LibraryContext;

namespace LibraryServices.Services.Create
{
    public static class CreateBooking
    {
        public static async Task AddBooking(Booking booking)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {

                    context.Books.Attach(booking.Book);
                    await context.Bookings.AddAsync(booking);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving booking for user '{booking.User}': {ex.Message}");
            }
        }
    }
}
