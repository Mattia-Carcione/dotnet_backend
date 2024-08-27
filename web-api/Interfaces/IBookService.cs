

using Model.Entities;

/**
*TODO:
*4. Implementare una classe di servizio chiamata LibroService con i seguenti metodi
*a. Prenota
*b. Consegna
*/
namespace Interfaces;

public interface IBookService
{
    Task<Booking> BookingAsync(string user, int bookId);
    Task DeliveryAsync(string user, int bookingId, int bookId);
}
