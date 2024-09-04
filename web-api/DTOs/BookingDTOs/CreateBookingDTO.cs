using System.ComponentModel.DataAnnotations;

namespace DTOs.BookingDTOs;

/// <summary>
/// Represents the DTO for creating new booking.
/// </summary>
public class CreateBookingDTO
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
    public string Email {get; set;} = string.Empty;

    /// <summary>
    /// Gets or sets the id of the book that is being booked.
    /// </summary>
    /// <value>
    /// The ID of the associated book.
    /// </value>
    /// <remarks>
    /// This field is required and is a foreign key to the <see cref="Book"/> entity.
    /// </remarks>
    public int BookId {get; set;}
}
