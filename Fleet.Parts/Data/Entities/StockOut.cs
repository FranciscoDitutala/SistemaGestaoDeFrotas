using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data;

public class StockOut
{
    public int Id { get; set; }
    public DateTime RegisteredDate { get; set; }

    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string RegisteredBy { get; set; } = default!;
    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string RequestedBy { get; set; } = default!;

    public DateTime? ApprovedDate { get; set; } = default!;
    [StringLength(31)]
    public string? ApprovedBy { get; set; } = default!;

    public DateTime? DeliveredDate { get; set; } = default!;
    [StringLength(31)]
    public string? DeliveredBy { get; set; } = default!;
    [StringLength(31)]
    public string? DeliveredTo { get; set; } = default!;

    public DateTime? CancelledDate { get; set; } = default!;
    [StringLength(31)]
    public string? CancelledBy { get; set; } = default!;

    public DateTime LastUpdated { get; set; }
    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string LastUpdatedBy { get; set; } = default!;

    [Required]
    [StringLength(2047)]
    public string Notes { get; set; } = default!;

    [MaxLength(1024)]
    public List<StockOutLine> RequestedLines { get; set; } = new();

    [MaxLength(1024)]
    public List<StockOutLine> ApprovedLines { get; set; } = new();

    [MaxLength(1024)]
    public List<DocumentMetadata> Documents { get; set; } = new();

    public override bool Equals(object? obj)
    {
        return obj is StockOut @out &&
               Id == @out.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }
}
