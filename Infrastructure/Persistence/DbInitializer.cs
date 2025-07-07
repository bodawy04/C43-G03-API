using Domain.Models.Identity;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Persistence.Identity;

namespace Persistence;

public class DbInitializer(StoreDbContext context,StoreIdentityDbContext storeIdentityDbContext,
    UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager): IDbInitializer
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

    public async Task InitializeIdentityAsync()
    {
        ///if ((await storeIdentityDbContext.Database.GetPendingMigrationsAsync()).Any())
        ///    await storeIdentityDbContext.Database.MigrateAsync();

        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        }
        if (!userManager.Users.Any())
        {
            var superAdmin = new ApplicationUser
            {
                DisplayName="Super Admin",
                Email="SuperAdmin@gmail.com",
                UserName="SuperAdmin",
                PhoneNumber="0123456789"
            };

            var admin = new ApplicationUser
            {
                DisplayName = "Admin",
                Email = "Admin@gmail.com",
                UserName = "Admin",
                PhoneNumber = "0123456789"
            };

            await userManager.CreateAsync(superAdmin,"Passw0rd");
            await userManager.CreateAsync(admin, "Passw0rd");

            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
            await userManager.AddToRoleAsync(admin, "Admin");
        }
    }
}
