//TODO:
// I servizi che non soddisfano le condizioni di cui sopra devono lanciare un’eccezione 
// custom di tipo PrenotazioneException contenente un identificativo (enum) 
// dell’errore riscontrato e il libro su cui si sta lavorando

namespace Exceptions;

public class BookingException : Exception
{
    public enum Exceptions
    {
        BookNotAvailable,
        ExistingBooking,
        ToManyBookings,
        UserFieldIsRequired,
        UserMismatch,
        BookAlreadyReturned,
        BookMismatch,
        BookNotFound
    }

    public Exceptions _error;

    public BookingException(Exceptions error) : base($"An error occurred while booking/delivering: {error}")
    {
        _error = error;
    }
}
