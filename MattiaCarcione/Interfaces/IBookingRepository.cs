using Model.Entities;

namespace Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>> SearchBookingsAsync(string user, Book book, DateTime deliveryDate = default);
}
