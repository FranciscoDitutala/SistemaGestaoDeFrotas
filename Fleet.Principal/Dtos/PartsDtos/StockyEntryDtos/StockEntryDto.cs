using Fleet.Principal.Dtos.PartsDtos.Owned;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Fleet.Principal.Dtos.PartsDtos.StockyEntryDtos
{
    public class StockEntryDto
    {
        public int Id { get; set; }
        public DateTime RegisteredDate { get; set; }

        public string RegisteredBy { get; set; } = default!;

        public DateTime LastUpdated { get; set; }
        public string LastUpdatedBy { get; set; } = default!;

        public string ProvidersInfo { get; set; } = default!;

        public string Notes { get; set; } = default!;

        public decimal TotalValue { get; set; }

        public List<StockEntryLine> Lines { get; set; } = new();

        
        [MaxLength(1024)]
        public List<DocumentMetadata> Documents { get; set; } = new();

        public override bool Equals(object? obj)
        {
            return obj is StockEntryDto entry &&
                   Id == entry.Id &&
                   EqualityComparer<List<DocumentMetadata>>.Default.Equals(Documents, entry.Documents);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Documents);
        }
        
    }
}
