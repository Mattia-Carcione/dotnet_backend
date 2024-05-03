using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryModel.Model;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices.Services.Delete
{
    public static class DeleteBooking
    {
        public static async Task RemoveBooking(int id)
        {
            try
            {
                using (var context = new LibraryDBContext())
                {
                    var booking = await context.Bookings.Where(a => a.BookingID == id).FirstAsync();
                    context.Bookings.Remove(booking);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting booking '{id}' ID: {ex.Message}");
            }
        }
    }
}
