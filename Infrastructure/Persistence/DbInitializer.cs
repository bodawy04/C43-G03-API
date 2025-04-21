using Domain.Models.Products;

namespace Persistence;

public class DbInitializer(StoreDbContext context) : IDbInitializer
{
    public async Task InitializeAsync()
    {
        try
        {
            /// Production
            ///if ((await context.Database.GetPendingMigrationsAsync()).Any())
            ///    await context.Database.MigrateAsync();

            ///Dev
            if (!context.Set<ProductBrand>().Any())
            {
                //Read Data from file
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\brands.json");
                //Convert to C# Objects [Deserialize]
                var objects = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                //Save to Db
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductBrand>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<ProductType>().Any())
            {
                //Read Data from file
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\types.json");
                //Convert to C# Objects [Deserialize]
                var objects = JsonSerializer.Deserialize<List<ProductType>>(data);
                //Save to Db
                if (objects is not null && objects.Any())
                {
                    context.Set<ProductType>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Set<Product>().Any())
            {
                //Read Data from file
                var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeding\products.json");
                //Convert to C# Objects [Deserialize]
                var objects = JsonSerializer.Deserialize<List<Product>>(data);
                //Save to Db
                if (objects is not null && objects.Any())
                {
                    context.Set<Product>().AddRange(objects);
                    await context.SaveChangesAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
