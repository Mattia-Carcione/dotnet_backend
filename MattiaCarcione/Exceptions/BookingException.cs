//TODO:
// I servizi che non soddisfano le condizioni di cui sopra devono lanciare un’eccezione 
// custom di tipo PrenotazioneException contenente un identificativo (enum) 
// dell’errore riscontrato e il libro su cui si sta lavorando

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
