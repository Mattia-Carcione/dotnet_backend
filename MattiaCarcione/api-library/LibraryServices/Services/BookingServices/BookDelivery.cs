using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryContext;
using LibraryServices.Services.Read.ReadBooking;
using LibraryServices.Services.Update;

namespace LibraryServices.Services.BookingServices
{
    public static class BookDelivery
    {
        public static async Task Delivery(int bookingID, int bookID)
        {
            try
            {
                var user = await ReadUserByBooking.SearchUserByBooking(bookingID);

                var userBookings = await ReadBookingsByUser.SearchBookingsByUser(user);

                bool isUserBookings = userBookings.Any(booking => booking.BookingID == bookingID && booking.DeliveryDate == default);

                if (isUserBookings)
                {
                    var booking = userBookings.Where(booking => booking.BookingID == bookingID && booking.Book.BookID == bookID).FirstOrDefault();

                    if (booking != null && booking.Book != null)
                    {
                        if (booking.Book.TotalCopiesLeft + 1 > booking.Book.TotalCopies)
                        {
                            booking.Book.TotalCopiesLeft = booking.Book.TotalCopies;
                        }
                        else
                        {
                            booking.Book.TotalCopiesLeft += 1;
                        }
                        booking.DeliveryDate = DateTime.Now;
                        await UpdateBooking.EditBooking(booking);

                        return;
                    }
                    else
                    {
                        throw new Exception($"An error occurred while searching booking: the book hasn't booking for user '{user}'");

                    }
                }
                throw new Exception($"There aren't booking with '{bookingID}' ID for the user '{user}' or the book has already delivery.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Errore while delivering: {ex.Message}");
            }
        }
    }
}
