namespace Model.Entities;

public class Booking
{
    public int ID {get; set;}
    public required string User {get; set;}
    public DateTime BookingDate {get; set;} = DateTime.Now;
    public DateTime DeliveryDate {get; set;}
    public Book? Book {get; set;}
}
