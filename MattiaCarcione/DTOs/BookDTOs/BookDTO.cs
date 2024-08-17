using DTOs.CategoryDTOs;

namespace DTOs.BookDTOs;

public class BookDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int Pages { get; set; }
    public int TotalCopies { get; set; }
    public int Copies { get; set; }
    public DateTime PublicationDate { get; set; }
    public required string Author { get; set; }
    public required string Editor { get; set; }
    public List<CategoryDTO> Categories{ get; set; } = new();
}
