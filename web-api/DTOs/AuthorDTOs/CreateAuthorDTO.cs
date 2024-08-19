using System.ComponentModel.DataAnnotations;

namespace DTOs.AuthorDTOs;

public class CreateAuthorDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate {get; set;}
}
