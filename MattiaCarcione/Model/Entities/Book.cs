namespace Model.Entities;

public class Book
{
    public int ID {get; set;}
    public required string Title {get; set;}
    public int Pages {get; set;}
    public int TotalCopies {get; set;}
    public int Copies {get; set;}
    public DateTime PublicationDate {get; set;}
    public Author? Author {get; set;}
    public List<Category>? Categories {get; set;}
    public Booking? Booking {get; set;}
    public Editor? Editor {get; set;}
}
