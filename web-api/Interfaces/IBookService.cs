using Models.Entities;

/**
*TODO:
*4. Implementare una classe di servizio chiamata LibroService con i seguenti metodi
*a. Prenota
*b. Consegna
*/
namespace Interfaces;

/// <summary>
/// Defines a contract that exposes the methods related to booking and returns.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Books a specific book for a user.
    /// </summary>
    /// <param name="user">The name of the user making booking.</param>
    /// <param name="bookId">The id of the book being booked</param>
    /// <returns>A task representing the asynchronous operation, with a <see cref="Booking"/> object containing the booking details.</returns>
    Task<Booking> BookingAsync(string user, int bookId);

    /// <summary>
    /// Updates the booking status when a book is returned by a user.
    /// </summary>
    /// <param name="user">The name of the user returning the book.</param>
    /// <param name="bookingId">The id of the booking.</param>
    /// <param name="bookId"> The id of the book being returned.</param>
    /// <returns>A task representing the asynchronous updating operation in the current context.</returns>
    Task UpdateBookingAsync(string user, int bookingId, int bookId);
}
