using System.ComponentModel.DataAnnotations;
using DTOs.BookDTOs;

namespace DTOs.EditorDTOs;

/// <summary>
/// Represents the DTO of the editor.
/// </summary>
public class EditorDTO
{
    /// <summary>
    /// Gets or sets the unique identifier of the editor.
    /// </summary>
    /// <value>
    /// The unique identifier of the editor
    /// </value>
    [Key]
    public int Id { get; set; }

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

    /// <summary>
    /// A editor's collection of the <see cref="BookDTO"/>
    /// </summary>
    public ICollection<BookDTO> Books {get; set;} = new List<BookDTO>();
}
