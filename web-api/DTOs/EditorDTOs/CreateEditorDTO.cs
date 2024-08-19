using System.ComponentModel.DataAnnotations;

namespace DTOs.EditorDTOs;

public class CreateEditorDTO
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}
