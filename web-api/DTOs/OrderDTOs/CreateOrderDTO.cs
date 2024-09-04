using System.ComponentModel.DataAnnotations;

namespace DTOs.OrderDTOs
{
    /// <summary>
    /// The DTO for creating new order.
    /// </summary>
    public class CreateOrderDTO
    {
        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <value>
        /// The email address of the user.
        /// </value>
        /// <remarks>
        /// The field is required.
        /// </remarks>
        [Required]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the unique identifier of the specified book.
        /// </summary>
        /// <value>
        /// The unique identifier of the specified book.
        /// </value>
        /// <remarks>
        /// The field is required.
        /// </remarks>
        [Required]
        public int BookId { get; set; }
    }
}
