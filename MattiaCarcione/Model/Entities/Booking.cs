/**
*TODO:
*Prenotazione
*-Utente: string
*-DataPrenotazione : Date
*-DataRestituzione : Date
*/

namespace Model.Entities;

public class Booking
{
    private int id;
    public int Id {get {return id;} set {id = value;}}

    private string? user;
    public string? User {get {return user;} set {user = value;}}

    private DateTime bookingDate;
    public DateTime BookingDate {get {return bookingDate;} set {bookingDate = DateTime.Now;}}

    private DateTime deliveryDate;
    public DateTime DeliveryDate {get {return deliveryDate;} set {deliveryDate = value;}}
    
    public Book? Book {get; set;}
}
