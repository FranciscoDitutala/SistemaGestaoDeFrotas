

namespace Fleet.Principal.Dtos.CommonDtos.ModelDtos
{
    public class VehicleModelDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public string Acronym { get; set; } = default!;
        public int VehicleBrandId { get; set; }
        public bool Enabled { get; set; }

    }
}
