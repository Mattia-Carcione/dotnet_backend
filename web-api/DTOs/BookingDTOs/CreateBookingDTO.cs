using System.ComponentModel.DataAnnotations;

namespace DTOs.BookingDTOs;

/// <summary>
/// Represents the DTO for creating new booking.
/// </summary>
public class CreateBookingDTO
{
    /// <summary>
    /// Gets or sets the user making the booking.
    /// </summary>
    /// <value>
    /// The user making the booking.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MaxLength(50), MinLength(5)]
    public string User {get; set;} = string.Empty;

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
