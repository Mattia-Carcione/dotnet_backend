namespace DTOs.BookingDTOs;

public class CreateBookingDTO
{
    public string? User {get; set;}

    public DateTime BookingDate {get; set;}

    public DateTime DeliveryDate {get; set;}
}
