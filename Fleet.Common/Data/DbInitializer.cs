using System.Text.Json;

namespace Fleet.Common.Data;

public static class DbInitializer
{
    public static async Task EnsurePopulated(IApplicationBuilder app)
    {
        FleetCommonDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<FleetCommonDbContext>();

        //if (context.Database.GetPendingMigrations().Any())
        //{
        //    context.Database.Migrate();
        //}

        if (!context.VehicleBrands.Any())
        {
            var brandList = JsonSerializer.Deserialize<BrandMetadata[]>(await FleetDataTools.GetEmbeddedResourceStr<FleetCommonDbContext>("Brands.json"));
            if (brandList is not null)
            {
                await context.VehicleBrands.AddRangeAsync(await Task.WhenAll(brandList.OrderBy(b => b.Name).Select(async b => new VehicleBrand
                {
                    Company = b.Company,
                    Name = b.Name,
                    Logo = await FleetDataTools.GetEmbeddedResourceBytes<FleetCommonDbContext>(b.Logo),
                    Enabled = true
                })));
                await context.SaveChangesAsync();
            }
        }
    }
}

internal record BrandMetadata(string Logo, string Name, string Company);
