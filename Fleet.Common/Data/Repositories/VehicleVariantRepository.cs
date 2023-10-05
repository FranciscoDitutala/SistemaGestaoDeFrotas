namespace Fleet.Common.Data;

public class VehicleVariantRepository : EntityRepository<VehicleVariant, int>
{
    public VehicleVariantRepository(FleetCommonDbContext context) : base(context)
    {
    }
}
