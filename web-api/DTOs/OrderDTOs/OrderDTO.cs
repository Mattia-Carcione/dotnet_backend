using DTOs.BookDTOs;
using DTOs.UserDTOs;

namespace DTOs.OrderDTOs
{
    /// <summary>
    /// Represents the DTO of the oreder.
    /// </summary>
    public class OrderDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order.
        /// </summary>
        /// <value>
        /// The unique identifier of the order.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the order.
        /// </summary>
        /// <value>
        /// The creation date of the order.
        /// </value>
        /// <remarks>
        /// Usually set to <see cref="DateTime.Now"/> when the order was created.
        /// </remarks>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the user making the order.
        /// </summary>
        /// <value>
        /// The user making the order.
        /// </value>
        public UserDTO? User { get; set; }

        /// <summary>
        /// Gets or sets the specified book.
        /// </summary>
        /// <value>
        /// The specified book.
        /// </value>
        public BookDTO? Book { get; set; }
    }
}
