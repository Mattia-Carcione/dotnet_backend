using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    /// <summary>
    /// Represents the order entity of the context.
    /// </summary>
    public class Order
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
        /// Represents the relationshipt between the <see cref="Order"/> and <see cref="User"/>. The field is required.
        /// </remarks>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the user making the order.
        /// </summary>
        /// <value>
        /// The user making the order.
        /// </value>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the specified book.
        /// </summary>
        /// <value>
        /// The unique identifier of the specified book.
        /// </value>
        /// <remarks>
        /// Represents the relationshipt between the <see cref="Order"/> and <see cref="Book"/>. The field is required.
        /// </remarks>
        [Required]
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the specified book.
        /// </summary>
        /// <value>
        /// The specified book.
        /// </value>
        public Book? Book { get; set; }
    }
}
