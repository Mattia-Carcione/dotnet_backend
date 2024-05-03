using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryExceptions
{
    public class BookingExceptions : Exception
    {
        public enum BookingException
        {
            BookSoldOut,
            UserAlreadyHasThreeBooking,
            UserAlreadyBookedSameBook,
        }
        public BookingException Error { get; }
        public BookingExceptions() { }
        public BookingExceptions(BookingException bookingException) : base($"An error occurred: {bookingException}")
        {
            Error = bookingException;
        }
        public BookingExceptions(string bookTitle, BookingException bookingException) : base($"An error occured with book '{bookTitle}': {bookingException}")
        {
            Error = bookingException;
        }
        public BookingExceptions(string message, Exception innerException) : base(message, innerException) { }

    }
}
