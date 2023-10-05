using Fleet.Principal.Dtos.PartsDtos.Owned;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.PartDtos
{
    public class PartDto
    {

        public string UPC { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string PartTypeName { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<VehicleVariantFilter> VariantFilters { get; set; } = new();
        public decimal StockQty { get; set; }
    }
}
