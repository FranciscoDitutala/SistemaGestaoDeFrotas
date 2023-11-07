using Fleet.Principal.Dtos.PartsDtos.Owned;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.StockOutDtos
{
    public class StockOutDto
    {
        public int Id { get; set; }
        public DateTime RegisteredDate { get; set; }

        public string RegisteredBy { get; set; } = default!;

        public string RequestedBy { get; set; } = default!;

        public DateTime? ApprovedDate { get; set; } = default!;

        public string? ApprovedBy { get; set; } = default!;

        public DateTime? DeliveredDate { get; set; } = default!;

        public string? DeliveredBy { get; set; } = default!;

        public string? DeliveredTo { get; set; } = default!;

       public DateTime? CancelledDate { get; set; } = default!;
      
        public string? CancelledBy { get; set; } = default!;

        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; } = default!;

        public string Notes { get; set; } = default!;

        public List<StockOutLine> RequestedLines { get; set; } = new();

        public List<StockOutLine> ApprovedLines { get; set; } = new();

        public List<DocumentMetadata> Documents { get; set; } = new();

       
    }
}
