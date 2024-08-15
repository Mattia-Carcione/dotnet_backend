using Model.Entities;

namespace Interfaces;

public interface IBookingService
{
    Task BookingAsync(string user, int bookId);
    Task DeliveryAsync(int bookingId);
}
