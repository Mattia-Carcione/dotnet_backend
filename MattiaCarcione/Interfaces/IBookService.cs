namespace Interfaces;

public interface IBookService
{
    Task BookingAsync(string user, int bookId);
    Task DeliveryAsync(int bookingId);
}
