namespace Fleet.Parts.Data;

public class PartCategoryRepository : EntityRepository<PartCategory, string>
{
    public PartCategoryRepository(FleetPartsDbContext context) : base(context)
    {
    }
}
