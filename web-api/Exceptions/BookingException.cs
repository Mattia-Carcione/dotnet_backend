//TODO:
// I servizi che non soddisfano le condizioni di cui sopra devono lanciare un’eccezione 
// custom di tipo PrenotazioneException contenente un identificativo (enum) 
// dell’errore riscontrato e il libro su cui si sta lavorando

namespace Exceptions;
/// <summary>
/// Represents errors thar occur during booking process.
/// </summary>
public class BookingException : Exception
{
    /// <summary>
    /// Represents the types of the booking related errors.
    /// </summary>
    public enum Exceptions
    {
        /// <summary>
        /// Indicates that the requested book is not available
        /// </summary>
        BookNotAvailable,

        /// <summary>
        /// Indicates that there is an exisisting booking for the item.
        /// </summary>
        ExistingBooking,

        /// <summary>
        /// Indicates that the specified user has exceeded the maximum allowed booking, max 3 per user.
        /// </summary>
        ToManyBookings,

        /// <summary>
        /// Indicates that the user field is missing.
        /// </summary>
        UserFieldIsRequired,

        /// <summary>
        /// Indicates that there is a mismatch between the user and the booking.
        /// </summary>
        UserMismatch,

        /// <summary>
        /// Indicates that the book for specified booking is already been returned.
        /// </summary>
        BookAlreadyReturned,

        /// <summary>
        /// Indicates that there is a mismatch between the book and the booking.
        /// </summary>
        BookMismatch,

        /// <summary>
        /// Indicates that the requested book is not found.
        /// </summary>
        BookNotFound,

        /// <summary>
        /// Indicates that the requested booking is not found.
        /// </summary>
        BookingNotFound
    }

    /// <summary>
    /// The <see cref="Exceptions"/> type of booking related error.
    /// </summary>
    public Exceptions _error;

    /// <summary>
    /// Initializes a new instance of <see cref="BookingException"/> using the specified type of booking related error.
    /// </summary>
    /// <param name="error"></param>
    public BookingException(Exceptions error) : base($"An error occurred while booking: {error}")
    {
        _error = error;
    }
}
