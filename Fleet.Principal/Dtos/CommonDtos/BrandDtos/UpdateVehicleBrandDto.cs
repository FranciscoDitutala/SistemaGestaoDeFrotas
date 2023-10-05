using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.CommonDtos.BrandDtos
{
    public class UpdateVehicleBrandDto
    {
        public string Company { get; set; } = default!;

        public string Name { get; set; } = default!;
        public IFormFile Logo { get; set; } = default!;
        public bool Enabled { get; set; }
    }
}
