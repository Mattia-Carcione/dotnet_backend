using System.ComponentModel.DataAnnotations;
using DTOs.BookingDTOs;
using DTOs.CategoryDTOs;

namespace DTOs.BookDTOs;

public class BookDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(50), MinLength(3)]
    public string Title { get; set; } = string.Empty;
    public int Pages { get; set; }
    public int TotalCopies { get; set; }
    public int Copies { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Author { get; set; } = string.Empty;
    public string Editor { get; set; } = string.Empty;
    public ICollection<CategoryDTO> Categories{ get; set; } = new List<CategoryDTO>();
    public ICollection<BookingDTO> Bookings{ get; set; } = new List<BookingDTO>();
}
