using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Prenotazione
*-Utente: string
*-DataPrenotazione : Date
*-DataRestituzione : Date
*/
namespace Models.Entities;

/// <summary>
/// Represents the entity booking of the context.
/// </summary>
[Table("Bookings")]
public class Booking
{
    /// <summary>
    /// Gets or sets the unique identifier of the booking.
    /// </summary>
    /// <value>
    /// The unique identifier of the booking
    /// </value>
    [Key]
    public int Id {get {return id;} set {id = value;}}
    private int id;

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
    [MaxLength(50), MinLength(3)]
    public string User {get {return user;} set {user = value;}}
    private string user = string.Empty;


    /// <summary>
    /// Gets or sets the date when creating new booking.
    /// </summary>
    /// <remarks>
    /// This date is typically set to the current date when creating a new booking.
    /// </remarks>
    /// <value>
    /// The date the booking was made. Defaults to <see cref="DateTime.Today"/>.
    /// </value>
    public DateTime BookingDate {get {return bookingDate;} set {bookingDate = value;}}
    private DateTime bookingDate;

    /// <summary>
    /// Gets or sets the date when the booked item is expected to be returned.
    /// </summary>
    /// <value>
    /// The expected return date of the booked item. Defaults to <see langword="default"/>.
    /// </value>
    public DateTime ReturnDate { get {return returnDate; } set { returnDate = value;}}
    private DateTime returnDate;

    /// <summary>
    /// Gets or sets the id of the book that is being booked.
    /// </summary>
    /// <value>
    /// The ID of the associated book.
    /// </value>
    /// <remarks>
    /// This field is required and is a foreign key to the <see cref="Book"/> entity.
    /// </remarks>
    [ForeignKey("Book")]
    [Required]
    public int BookId {get; set;}

    /// <summary>
    /// Gets or sets the book associated with the booking.
    /// </summary>
    /// <value>
    /// The book that is booked.
    /// </value>
    /// <remarks>
    /// This property represents the relationship between the booking and the <see cref="Book"/> entity.
    /// </remarks>
    public Book? Book {get; set;}
}
