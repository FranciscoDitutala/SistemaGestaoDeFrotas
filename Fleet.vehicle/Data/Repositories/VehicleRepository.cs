using Fleet.Data;
using Fleet.Transport.Data.Entities;

namespace Fleet.Transport.Data.Repositories
{
    public class VehicleRepository : EntityRepository<Vehicle, int>
    {
        public VehicleRepository(FleetTransportDbContext context) : base(context)
        {
        }
    }
}
