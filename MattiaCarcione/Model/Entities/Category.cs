using System;

namespace Model.Entities;

public class Category
{
    public int ID {get; set;}
    public required string Genre {get; set;}
    public string? Description {get; set;}
    public List<Book>? Books {get; set;}
}
