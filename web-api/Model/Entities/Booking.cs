using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Prenotazione
*-Utente: string
*-DataPrenotazione : Date
*-DataRestituzione : Date
*/
namespace Model.Entities;

[Table("Bookings")]
public class Booking
{
    private int id;
    [Key]
    public int Id {get {return id;} set {id = value;}}


    private string user = string.Empty;
    [Required]
    [MaxLength(50), MinLength(5)]
    public string User {get {return user;} set {user = value;}}

    private DateTime bookingDate;
    public DateTime BookingDate {get {return bookingDate;} set {bookingDate = value;}}

    private DateTime deliveryDate;
    public DateTime DeliveryDate {get {return deliveryDate;} set {deliveryDate = value;}}
    
    [ForeignKey("Book")]
    public int BookId {get; set;}
    [Required]
    public Book Book {get; set;} = null!;
}
