namespace Fleet.Parts.Data;

public class StockOutRepository : EntityRepository<StockOut, int>
{
    public StockOutRepository(FleetPartsDbContext context) : base(context)
    {
    }
}