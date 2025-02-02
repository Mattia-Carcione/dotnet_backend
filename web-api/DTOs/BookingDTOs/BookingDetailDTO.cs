using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;
using DTOs.UserDTOs;

namespace DTOs.BookingDTOs;

/// <summary>
/// Represents DTO for the booking's detail.
/// </summary>
public class BookingDetailDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the booking.
    /// </summary>
    /// <value>
    /// The unique identifier of the booking
    /// </value>
    [Key]
    public int Id {get; set;}

    /// <summary>
    /// Gets or sets the user making the booking.
    /// </summary>
    /// <value>
    /// The user making the booking.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the booking and the <see cref="UserDTO"/> entity.
    /// </remarks>
    [Required]
    public UserDTO? User { get; set; }

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

    /// <summary>
    /// Gets or sets the book associated with the booking.
    /// </summary>
    /// <value>
    /// The book that is booked.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the booking and the <see cref="BookDTO"/> entity.
    /// </remarks>
    [Required]
    public BookDTO? Book {get; set;}
}
