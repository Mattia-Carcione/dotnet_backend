/**
*TODO:
*Autore
*+Nome: string
*+Cognome : int 
*+DataNascita : Date
*/

namespace Model.Entities;

public class Author
{
    public int ID {get; set;}
    public required string Name {get; set;}
    public required string LastName {get; set;}
    public DateTime BirthDate {get; set;}
    public List<Book> Books {get; set;} = new();
}
