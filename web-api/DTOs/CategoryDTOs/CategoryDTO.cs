using System.ComponentModel.DataAnnotations;

namespace DTOs.CategoryDTOs;

public class CategoryDTO
{
    [Key]
    public int Id {get; set;}

    [Required]
    [MaxLength(50)]
    public string Genre {get; set;} = string.Empty;

    [MaxLength(400)]
    public string Description {get; set;} = string.Empty;
}
