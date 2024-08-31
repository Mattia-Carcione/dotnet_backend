using System.ComponentModel.DataAnnotations;

namespace DTOs.EditorDTOs;

/// <summary>
/// Represents DTO for creating a new editor.
/// </summary>
public class CreateEditorDTO
{
    /// <summary>
    /// Gets or sets the name of the editor.
    /// </summary>
    /// <value>
    /// The name of the editor.
    /// </value>
    /// <remarks>
    /// The field is required. Min legth: 3, max length: 50.
    /// </remarks>
    [Required]
    [MaxLength(50), MinLength(3)]
    public string Name { get; set; } = string.Empty;
}
