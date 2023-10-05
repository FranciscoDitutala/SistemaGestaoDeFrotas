namespace Fleet.Parts.Data;

public class PartRepository : EntityRepository<Part, string>
{
    public PartRepository(FleetPartsDbContext context) : base(context)
    {
    }
}
