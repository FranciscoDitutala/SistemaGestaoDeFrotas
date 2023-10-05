using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data;

public class DocumentMetadata
{
    [Required]
    [StringLength(63, MinimumLength = 1)]
    public string DocumentId { get; set; } = default!;
    [Required]
    [StringLength(63, MinimumLength = 4)]
    public string Name { get; set; } = default!;
    [Required]
    [StringLength(63, MinimumLength = 4)]
    public string FileName { get; set; } = default!;
    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string MimeType { get; set; } = default!;
    [Required]
    [StringLength(2043)]
    public string Description { get; set; } = default!;
}
