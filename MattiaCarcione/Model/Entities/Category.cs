/**
*TODO:
*Categoria
*-Genere: string
*-Descrizione: string
*/

namespace Model.Entities;

public class Category
{
    private int id {get; set;}
    public int Id {get {return id;} set {id = value;}}

    private string? genre {get; set;}
    public string? Genre {get {return genre;} set {genre = value;}}

    private string? description {get; set;}
    public string? Description {get {return description;} set {description = value;}}
    
    public List<Book> Books {get; set;} = new();
}
