using System.ComponentModel.DataAnnotations;

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
}
