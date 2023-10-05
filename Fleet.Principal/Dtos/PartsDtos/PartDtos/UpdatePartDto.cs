namespace Fleet.Principal.Dtos.PartsDtos.PartDtos
{
    public class UpdatePartDto
    {
        public string UPC { get; set; } = string.Empty;

        public string SKU { get; set; } = string.Empty;

        public string PartTypeName { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal StockQty { get; set; }
    }
}
