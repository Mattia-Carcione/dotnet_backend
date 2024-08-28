using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/**
*TODO:
*Editore
*-Nome: string
*/

namespace Models.Entities;

[Table("Editors")]
public class Editor
{
    private int id {get; set;}
    [Key]
    public int Id {get {return id;} set {id = value;}}

    private string name {get; set;} = string.Empty;
    [Required]
    [MaxLength(50)]
    public string Name {get {return name;} set {name = value;}}

    public ICollection<Book> Books {get; set;} = new List<Book>();
}
