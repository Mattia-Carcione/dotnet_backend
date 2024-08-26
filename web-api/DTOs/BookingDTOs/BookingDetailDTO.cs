using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;

namespace DTOs.BookingDTOs;

public class BookingDetailDTO
{
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(50), MinLength(5)]
    public string User {get; set;} = string.Empty;

    public DateTime BookingDate {get; set;}

    public DateTime DeliveryDate {get; set;}

    [Required]
    public BookDTO Book {get; set;} = null!;
}
