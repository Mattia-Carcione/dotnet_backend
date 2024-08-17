using DTOs.BookDTOs;

namespace DTOs.BookingDTOs;

public class BookingDTO
{
    public int Id {get; set;}

    public string? User {get; set;}

    public DateTime BookingDate {get; set;}

    public DateTime DeliveryDate {get; set;}
    
    public BookDTO? Book {get; set;}
}
