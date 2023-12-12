using Fleet.Principal.Dtos.PartsDtos.Owned;

namespace Fleet.Principal.Dtos.PartsDtos.StockOutDtos
{
    public class StockOutApproveDto
    {
        public int Id { get; set; }
        public List<StockOutLine> ApprovedLines { get; set; } = new();

    }
}
