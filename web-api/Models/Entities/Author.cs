using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Autore
*+Nome: string
*+Cognome : int 
*+DataNascita : Date
*/
namespace Models.Entities;

[Table("Authors")]
public class Author
{
    [Key]
    public int Id {get; set;}
    [Required]
    public string Name {get; set;} = string.Empty;
    [Required]
    public string LastName {get; set;} = string.Empty;
    public DateTime BirthDate {get; set;}
    public ICollection<Book> Books {get; set;} = new List<Book>();
}
