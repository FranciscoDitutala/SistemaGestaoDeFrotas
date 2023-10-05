using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.Owned;

public class StockOutLine
{
    [StringLength(31, MinimumLength = 7)]
    public string PartUPC { get; set; } = null!;

    [Precision(27, 9)]
    public decimal Quantity { get; set; }
}
