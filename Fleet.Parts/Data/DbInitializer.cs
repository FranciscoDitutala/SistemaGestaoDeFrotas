using System.Text.Json;

namespace Fleet.Parts.Data;

public static class DbInitializer
{
    public static async Task EnsurePopulated(IApplicationBuilder app)
    {
        FleetPartsDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<FleetPartsDbContext>();

        context.Database.EnsureCreated();

        if (!context.PartCategories.Any())
        {
            var categoryList = JsonSerializer.Deserialize<PartCategoryMetadata[]>(await FleetDataTools.GetEmbeddedResourceStr<FleetPartsDbContext>("categories.json"));
            if (categoryList is not null)
            {
                foreach (var c in categoryList)
                {
                    var category = new PartCategory
                    {
                        Name = c.Name.Trim(),
                        Image = await FleetDataTools.GetEmbeddedResourceBytes<FleetPartsDbContext>(c.Image)
                    };

                    context.PartCategories.Add(category);

                    foreach (var t in c.PartTypes)
                    {
                        var type = context.PartTypes.Find(t.Name.Trim()) ?? context.PartTypes.Add(new PartType
                        {
                            Name = t.Name.Trim(),
                            Image = await FleetDataTools.GetEmbeddedResourceBytes<FleetPartsDbContext>(t.Image)
                        }).Entity;

                        type.PartTypeCategories.Add(new PartTypeCategory
                        {
                            PartCategory = category,
                            PartType = type,
                            SubCategory = t.SubCategory.Trim()
                        });
                    }
                }
                await context.SaveChangesAsync();
            }
        }
    }
}

internal record PartTypeMetadata(string Name, string Image, string SubCategory);

internal record PartCategoryMetadata(string Name, string Image, PartTypeMetadata[] PartTypes);

