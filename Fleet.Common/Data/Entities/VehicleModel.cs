using System.ComponentModel.DataAnnotations;

namespace Fleet.Common.Data;

public class VehicleModel
{
    public int Id { get; set; }

    [Required]
    [StringLength(127)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(31)]
    public string Acronym { get; set; } = default!;

    public int VehicleBrandId { get; set; }


public bool Enabled { get; set; }

    [MaxLength(1024)]
    public List<VehicleVariant> Variants { get; set; } = new();
}
