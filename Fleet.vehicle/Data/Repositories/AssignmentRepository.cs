using Fleet.Data;
using Fleet.Transport.Data.Entities;

namespace Fleet.Transport.Data.Repositories
{
    public class AssignmentRepository : EntityRepository<Assignment, int>
    {
        public AssignmentRepository(FleetTransportDbContext context) : base(context)
        {
            
        }
    }
}
