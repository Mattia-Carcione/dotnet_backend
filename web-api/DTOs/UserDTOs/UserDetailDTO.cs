using DTOs.BookingDTOs;
using DTOs.OrderDTOs;
using System.ComponentModel.DataAnnotations;

namespace DTOs.UserDTOs
{
    /// <summary>
    /// DTO for showing the detail of user.
    /// </summary>
    public class UserDetailDTO
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
        /// Represents the relationship between <see cref="UserDetailDTO"/> and <see cref="BookingDTO"/>.
        /// </summary>
        /// <value>
        /// The booking collection of the user.
        /// </value>
        public ICollection<BookingDTO> Bookings { get; set; } = new List<BookingDTO>();

        /// <summary>
        /// Represents the relationship between <see cref="User"/> and <see cref="OrderDTO"/>.
        /// </summary>
        /// <value>
        /// The order collection of the user.
        /// </value>
        public ICollection<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }
}
