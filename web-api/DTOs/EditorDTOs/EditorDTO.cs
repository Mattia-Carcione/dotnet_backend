using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;

namespace DTOs.EditorDTOs;

public class EditorDTO
{
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public ICollection<BookDTO> Books {get; set;} = new List<BookDTO>();
}
