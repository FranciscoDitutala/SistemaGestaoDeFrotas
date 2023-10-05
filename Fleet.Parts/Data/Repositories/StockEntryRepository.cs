namespace Fleet.Parts.Data;

public class StockEntryRepository : EntityRepository<StockEntry, int>
{
    public StockEntryRepository(FleetPartsDbContext context) : base(context)
    {
    }
}
