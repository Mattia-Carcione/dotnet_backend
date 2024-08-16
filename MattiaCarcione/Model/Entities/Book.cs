/**
*TODO:
*Libro
*+Titolo: string
*+NumeroPagine : int 
*+DataPubblicazione : Date 
*+NumeroCopieTotali : int 
*+NumeroCopieRimaste : int
*
*+AddCategoria(in c : Categoria) 
*+RemoveCategoria(in c : Categoria) 
*/

namespace Model.Entities;

public class Book
{
    public int ID {get; set;}
    public required string Title {get; set;}
    public int Pages {get; set;}
    public int TotalCopies {get; set;}
    public int Copies {get; set;}
    public DateTime PublicationDate {get; set;}

    public void AddCategory(Category category) 
    {
        if(Categories != null && !Categories.Contains(category))
        {
            Categories.Add(category);
        }
    }

    public void RemoveCategory(Category category) 
    {
        if(Categories != null && !Categories.Contains(category))
        {
            Categories.Remove(category);
        }
    }

    public int AuthorId { get; set; }
    public Author? Author {get; set;}
    public List<Category>? Categories {get; set;}
    public List<Booking>? Bookings {get; set;}
    public int EditorId { get; set; }
    public Editor? Editor {get; set;}
}
