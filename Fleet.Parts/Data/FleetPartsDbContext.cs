using Microsoft.EntityFrameworkCore;

namespace Fleet.Parts.Data;

public class FleetPartsDbContext : DbContext
{
    public FleetPartsDbContext(DbContextOptions<FleetPartsDbContext> options)
    : base(options){}

    public DbSet<Part> Parts { get; set; }
    public DbSet<PartType> PartTypes { get; set; }
    public DbSet<PartCategory> PartCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Part>()
            .OwnsMany(part => part.VariantFilters, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            });

        modelBuilder.Entity<PartType>()
            .HasMany(partType => partType.PartCategories)
            .WithMany(partCategory => partCategory.PartTypes)
            .UsingEntity<PartTypeCategory>(
                j => j
                    .HasOne(partTypeCategory => partTypeCategory.PartCategory)
                    .WithMany(partCategory => partCategory.PartTypeCategories)
                    .HasForeignKey(partTypeCategory => partTypeCategory.PartCategoryName),
                j => j
                    .HasOne(partTypeCategory => partTypeCategory.PartType)
                    .WithMany(partType => partType.PartTypeCategories)
                    .HasForeignKey(partTypeCategory => partTypeCategory.PartTypeName));

        modelBuilder.Entity<StockEntry>()
            .OwnsMany(entry => entry.Lines, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .OwnsMany(entry => entry.Documents, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .Property(entry => entry.RegisteredDate)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<StockOut>()
            .OwnsMany(sout => sout.RequestedLines, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .OwnsMany(sout => sout.ApprovedLines, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .OwnsMany(sout => sout.Documents, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.ToJson();
            })
            .Property(sout => sout.RegisteredDate)
            .HasDefaultValueSql("getdate()");
    }
}
