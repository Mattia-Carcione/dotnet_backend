using Model.Entities;

namespace Exceptions;

public class BookingException : Exception
{
    public enum Exceptions
    {
        BookNotAvailable,
        ExistingBooking,
        ToManyBookings
    }

    public Exceptions _error;
    public Book _book;

    public BookingException(Exceptions error, Book book) : base($"An error occurred while booking {book.Title}: {error}")
    {
        _error = error;
        _book = book;
    }
}
