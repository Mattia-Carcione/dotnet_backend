using Models.Entities;

namespace Interfaces
{
    /// <summary>
    /// Defines a contract that exposes method to buy book.
    /// </summary>
    /// <remarks>
    /// This class extends <see cref="IBookService"/>.
    /// </remarks>
    public interface IPremiumBookService : IBookService
    {
        /// <summary>
        /// Buys a specified book for a user.
        /// </summary>
        /// <param name="email">The email address of the user buying a book.</param>
        /// <param name="bookId">The unique identifier of the specified book.</param>
        /// <returns>
        /// A task representing asynchronous operation that returns a new instance of <see cref="Order"/>.
        /// </returns>
        Task<Order> BuyBookAsync(string email, int bookId);
    }
}
