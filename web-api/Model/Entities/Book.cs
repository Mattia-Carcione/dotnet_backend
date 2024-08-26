using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

[Table("Books")]
public class Book
{
    [Key]
    public int Id {get; set;}
    [Required]
    [MaxLength(50), MinLength(3)]
    public string Title { get; set; } = string.Empty;
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
        if(Categories != null && Categories.Contains(category))
        {
            Categories.Remove(category);
        }
    }

    [ForeignKey("Author")]
    [Required]
    public int AuthorId { get; set; }
    public Author Author {get; set;} = null!;

    public ICollection<Category> Categories {get; set;} = new List<Category>();
    public ICollection<Booking> Bookings {get; set;} = new List<Booking>();
    
    [ForeignKey("Editor")]
    [Required]
    public int EditorId { get; set; }
    public Editor Editor {get; set;} = null!;
}
