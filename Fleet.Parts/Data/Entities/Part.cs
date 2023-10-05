using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Parts.Data;

public class Part
{
    //public int Id { get; set; }

    [Key]
    [StringLength(31, MinimumLength = 7)]
    public string UPC { get; set; } = string.Empty;

    [Required]
    [StringLength(31, MinimumLength = 4)]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [StringLength(127, MinimumLength = 1)]
    public string PartTypeName { get; set; } = string.Empty;

    [Required]
    [StringLength(127)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [StringLength(127)]
    public string Model { get; set; } = string.Empty;

    [Required]
    [StringLength(1023)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(1024)]
    public List<VehicleVariantFilter> VariantFilters { get; set; } = new();

    [Precision(27, 9)]
    public decimal StockQty { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Part part &&
               UPC == part.UPC;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(UPC);
    }
}
