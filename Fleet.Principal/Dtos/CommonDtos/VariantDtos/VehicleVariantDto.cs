using Fleet.Principal.Dtos.CommonDtos.Owned;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.CommonDtos.VariantDtos
{
    public class VehicleVariantDto
    {
        public int Id { get; set; }

        public int ReleaseYear { get; set; }


        public string Name { get; set; } = default!;

        public string TechnicalName { get; set; } = default!;

        public VehicleBodySpecs BodySpecs { get; set; } = new();
        public VehicleTechnicalSpecs TechnicalSpecs { get; set; } = new();

        public int VehicleModelId { get; set; }

        public bool Enabled { get; set; }
    }
}
