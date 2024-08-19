using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDTOs;

public class CreateCategoryDTO
{
    [Required]
    [MaxLength(50)]
    public string Genre {get; set;} = string.Empty;

    [MaxLength(400)]
    public string Description {get; set;} = string.Empty;
}
