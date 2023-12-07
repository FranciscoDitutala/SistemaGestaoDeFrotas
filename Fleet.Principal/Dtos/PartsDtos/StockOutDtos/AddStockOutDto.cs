using Fleet.Principal.Dtos.PartsDtos.Owned;

namespace Fleet.Principal.Dtos.PartsDtos.StockOutDtos
{
    public class AddStockOutDto
    {

        public string RequestedBy { get; set; } = default!;
        public string RegisteredBy { get; set; } = default!;
        public string Notes { get; set; } = default!;
        public List<StockOutLine> RequestedLines { get; set; } = new();

    }
}
