namespace DTOs.BookDTOs;

public class CreateBookDTO
{
    public required string Title { get; set; }
    public int Pages { get; set; }
    public int TotalCopies { get; set; }
    public int Copies { get; set; }
    public DateTime PublicationDate { get; set; }
}
