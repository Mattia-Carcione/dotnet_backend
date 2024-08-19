namespace DTOs.AuthorDTOs;

public class CreateAuthorDTO
{
    public required string Name {get; set;}
    public required string LastName {get; set;}
    public DateTime BirthDate {get; set;}
}
