using System.ComponentModel.DataAnnotations;

namespace DTOs.BookingDTOs;

/// <summary>
/// Represents the DTO for updating a return date of the booking.
/// </summary>
public class UpdateBookingDTO
{
    /// <summary>
    /// Gets or sets the user email address making the booking.
    /// </summary>
    /// <value>
    /// The user email address making the booking.
    /// </value>
    /// <remarks>
    /// The field is required.
    /// </remarks>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the id of the booking was made.
    /// </summary>
    /// <value>
    /// The ID of the booking.
    /// </value>
    /// <remarks>
    /// This property represents the id of the booking was made.
    /// </remarks>
    public int BookingId { get; set; }

    /// <summary>
    /// Gets or sets the id of the book was booked related to the <see cref="Booking"/>.
    /// </summary>
    /// <value>
    /// The ID of the book was booked.
    /// </value>
    /// <remarks>
    /// This property represents the id of the book was booked.
    /// </remarks>
    public int BookId { get; set; }
}
