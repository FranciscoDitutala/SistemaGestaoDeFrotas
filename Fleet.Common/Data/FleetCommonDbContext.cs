using Microsoft.EntityFrameworkCore;

namespace Fleet.Common.Data;

public class FleetCommonDbContext : DbContext
{
    public FleetCommonDbContext(DbContextOptions<FleetCommonDbContext> options)
    : base(options) { Database.EnsureCreated(); }

    public DbSet<VehicleBrand> VehicleBrands { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<VehicleVariant> VehicleVariants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<VehicleVariant>()
            .OwnsOne(variant => variant.BodySpecs, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .OwnsOne(variant => variant.TechnicalSpecs, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });
    }
}

