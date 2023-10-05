using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data;

public class StockEntry
{
    public int Id { get; set; }
    public DateTime RegisteredDate { get; set; }

    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string RegisteredBy { get; set; } = default!;

    public DateTime LastUpdated { get; set; }
    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string LastUpdatedBy { get; set; } = default!;

    [Required]
    [StringLength(1023)]
    public string ProvidersInfo { get; set; } = default!;

    [Required]
    [StringLength(2047)]
    public string Notes { get; set; } = default!;

    [Precision(27, 9)]
    public decimal TotalValue { get; set; }

    [MaxLength(1024)]
    public List<StockEntryLine> Lines { get; set; } = new();

    [MaxLength(1024)]
    public List<DocumentMetadata> Documents { get; set; } = new();

    public override bool Equals(object? obj)
    {
        return obj is StockEntry entry &&
               Id == entry.Id &&
               EqualityComparer<List<DocumentMetadata>>.Default.Equals(Documents, entry.Documents);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Documents);
    }
}
