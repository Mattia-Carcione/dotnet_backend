using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    /// <summary>
    /// Represents the user identity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        /// <value>
        /// The unique identifier of the user.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        /// <value>
        /// The username of the user.
        /// </value>
        /// <remarks>
        /// The field is required. Min length: 3, max length: 50
        /// </remarks>
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Username { get; set; } = string.Empty;

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
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the role of the user(premium or not).
        /// </summary>
        /// <value>
        /// The role of the user(premium or not).
        /// </value>
        /// <remarks>
        /// Defaults to <see langword="false"/>.
        /// </remarks>
        public bool IsPremium { get; set; } = false;

        /// <summary>
        /// Represents the relationship between <see cref="User"/> and <see cref="Booking"/>.
        /// </summary>
        /// <value>
        /// The booking collection of the user.
        /// </value>
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        /// <summary>
        /// Represents the relationship between <see cref="User"/> and <see cref="Order"/>.
        /// </summary>
        /// <value>
        /// The order collection of the user.
        /// </value>
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
