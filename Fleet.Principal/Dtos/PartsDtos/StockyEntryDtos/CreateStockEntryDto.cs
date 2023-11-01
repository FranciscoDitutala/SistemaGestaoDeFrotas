using Fleet.Principal.Dtos.PartsDtos.Owned;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos
{
    public class CreateStockEntryDto
    {

        public string ProvidersInfo { get; set; } = default!;

        public string Notes { get; set; } = default!;

        public decimal TotalValue { get; set; }

        public List<StockEntryLine> Lines { get; set; } = new();
        public List<DocumentMetadata> Documents { get; set; } = new();

    }
}
