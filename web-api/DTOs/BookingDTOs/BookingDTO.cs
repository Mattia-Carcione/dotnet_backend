using System.ComponentModel.DataAnnotations;

namespace DTOs.BookingDTOs;

/// <summary>
/// Represents the DTO of the booking.
/// </summary>
public class BookingDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the booking.
    /// </summary>
    /// <value>
    /// The unique identifier of the booking
    /// </value>
    [Key]
    public int Id { get; set; }

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
    /// Gets or sets the date when creating new booking.
    /// </summary>
    /// <remarks>
    /// This date is typically set to the current date when creating a new booking.
    /// </remarks>
    /// <value>
    /// The date the booking was made.
    /// <para>
    /// Defaults to <see cref="DateTime.Now"/>.
    /// </para>
    /// </value>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the booked item is expected to be returned.
    /// </summary>
    /// <value>
    /// The expected return date of the booked item.
    /// <para>
    /// Defaults to <see langword="default"/>.
    /// </para>
    /// </value>
    public DateTime ReturnDate { get; set; }
}
