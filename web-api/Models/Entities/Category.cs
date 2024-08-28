using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Categoria
*-Genere: string
*-Descrizione: string
*/

namespace Models.Entities;

[Table("Categories")]
public class Category
{
    private int id {get; set;}
    [Key]
    public int Id {get {return id;} set {id = value;}}

    private string genre {get; set;} = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Genre {get {return genre;} set {genre = value;}}

    private string description {get; set;} = string.Empty;
    [MaxLength(400)]
    public string Description {get {return description;} set {description = value;}}
    
    public ICollection<Book> Books {get; set;} = new List<Book>();
}
