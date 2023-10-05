using Fleet.Transport.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Transport.Data
{
    public class FleetTransportDbContext : DbContext
    {
        public FleetTransportDbContext(DbContextOptions<FleetTransportDbContext> options) : base(options)
        { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Document> Documents {get;set;}

        public DbSet<Assignment> Assignment { get; set; }


    }
}
