
using Fleet.Principal.Dtos.PartsDtos.Owned;

namespace Fleet.Principal.Dtos.PartsDtos.PartDtos
{
    public class AddPartDto
    {
        public string UPC { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string PartTypeName { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<VehicleVariantFilter> VariantFilters { get; set; } = new();
    }
}
