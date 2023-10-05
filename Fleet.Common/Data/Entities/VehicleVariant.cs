using System.ComponentModel.DataAnnotations;

namespace Fleet.Common.Data;

public class VehicleVariant
{
    public int Id { get; set; }

    public int ReleaseYear { get; set; }

    [Required]
    [StringLength(127)]
    public string Name { get; set; } = default!;

    [Required]
    [StringLength(31)]
    public string TechnicalName { get; set; } = default!;

    public VehicleBodySpecs BodySpecs { get; set; } = new();
    public VehicleTechnicalSpecs TechnicalSpecs { get; set; } = new();

    public int VehicleModelId { get; set; }

    public bool Enabled { get; set; }
}
