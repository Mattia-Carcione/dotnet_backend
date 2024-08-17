namespace DTOs.AuthorDTOs;

public class AuthorDTO
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public required string LastName {get; set;}
    public DateTime BirthDate {get; set;}
}
