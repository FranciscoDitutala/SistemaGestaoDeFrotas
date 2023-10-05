
namespace Fleet.Principal.Dtos.CommonDtos.BrandDtos
{
    public class VehicleBrandDto
    {

        public int Id { get; set; }


        public string Company { get; set; } = default!;

        public string Name { get; set; } = default!;

        public byte[] Logo { get; set; } = default!;
        public bool Enabled { get; set; }

    }
}
