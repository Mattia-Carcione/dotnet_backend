using DTOs.BookDTOs;
using DTOs.UserDTOs;
using System.ComponentModel.DataAnnotations;

namespace DTOs.OrderDTOs
{
    /// <summary>
    /// The DTO for showing the detail of order
    /// </summary>
    public class OrderDetailDTO
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
        /// Gets or sets the unique identifier of the user making the order.
        /// </summary>
        /// <value>
        /// The unique identifier of the user making the order.
        /// </value>
        /// <remarks>
        /// Represents the relationshipt between the <see cref="OrderDetailDTO"/> and <see cref="UserDTO"/>. The field is required.
        /// </remarks>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user making the order.
        /// </summary>
        /// <value>
        /// The user making the order.
        /// </value>
        public UserDTO? User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the specified book.
        /// </summary>
        /// <value>
        /// The unique identifier of the specified book.
        /// </value>
        /// <remarks>
        /// Represents the relationshipt between the <see cref="OrderDetailDTO"/> and <see cref="BookDTO"/>. The field is required.
        /// </remarks>
        [Required]
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the specified book.
        /// </summary>
        /// <value>
        /// The specified book.
        /// </value>
        public BookDTO? Book { get; set; }
    }
}
