using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;

namespace DTOs.AuthorDTOs;

public class AuthorDTO
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public ICollection<BookDTO> Books { get; set; } = new List<BookDTO>();
}
