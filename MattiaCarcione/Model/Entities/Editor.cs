/**
*TODO:
*Editore
*-Nome: string
*/

namespace Model.Entities;

public class Editor
{
    private int id {get; set;}
    public int Id {get {return id;} set {id = value;}}

    private string? name {get; set;}
    public string? Name {get {return name;} set {name = value;}}

    public List<Book>? Books {get; set;}
}
