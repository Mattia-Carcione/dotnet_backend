namespace Model.Entities;

public class Booking
{
    public int ID {get; set;}
    public required string User {get; set;}
    public required DateTime BookingDate {get; set;} = DateTime.Now;
    public DateTime DeliveryDate {get; set;}
    public List<Book>? Books {get; set;}
}
