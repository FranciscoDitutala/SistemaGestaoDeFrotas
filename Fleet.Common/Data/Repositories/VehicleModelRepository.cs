namespace Fleet.Common.Data;

public class VehicleModelRepository : EntityRepository<VehicleModel, int>
{
    public VehicleModelRepository(FleetCommonDbContext context) : base(context)
    {
    }
}

