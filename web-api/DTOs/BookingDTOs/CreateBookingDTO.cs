using System.ComponentModel.DataAnnotations;

namespace DTOs.BookingDTOs;

public class CreateBookingDTO
{
    [Required]
    [MaxLength(50), MinLength(5)]
    public string User {get; set;} = string.Empty;

    public int BookId {get; set;}
}
