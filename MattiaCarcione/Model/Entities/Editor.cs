using System;

namespace Model.Entities;

public class Editor
{
    public int ID {get; set;}
    public required string Name {get; set;}
    public List<Book>? Books {get; set;}
}
