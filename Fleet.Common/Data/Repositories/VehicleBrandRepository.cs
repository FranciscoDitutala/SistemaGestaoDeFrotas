namespace Fleet.Common.Data;

public class VehicleBrandRepository : EntityRepository<VehicleBrand, int>
{
    public VehicleBrandRepository(FleetCommonDbContext context) : base(context)
    {
    }
}
