using Domain.Contracts;
using Domain.Entites.Identity;
using Domain.Entites.Orders;
using Domain.Entites.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Periestence.Data.Contexts;
using Periestence.Identity.Contexts;
using System.Text.Json;

namespace Periestence
{
    public class DbInitializer(StoreDbContext _context
        , IdentityStoreDbContext _identity,
        UserManager<AppUser> _userManager,
        RoleManager<IdentityRole> _roleManager
        ) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if (_context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) await _context.Database.MigrateAsync();


            if (!await _context.ProductBrands.AnyAsync())
            {
                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null && brands.Count > 0) await _context.ProductBrands.AddRangeAsync(brands);


            }

            if (!await _context.ProductTypes.AnyAsync())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\types.json");

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types is not null && Types.Count > 0) await _context.ProductTypes.AddRangeAsync(Types);


            }

            if (!await _context.Products.AnyAsync())
            {
                var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (products is not null && products.Count > 0) await _context.Products.AddRangeAsync(products);

            }
            if (!await _context.DeliveryMethods.AnyAsync())
            {
                var DeliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\delivery.json");

                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (DeliveryMethods is not null && DeliveryMethods.Count > 0) await _context.DeliveryMethods.AddRangeAsync(DeliveryMethods);

            }

            await _context.SaveChangesAsync();


        }

        public async Task InitializeIdentityAsync()
        {
            if (_identity.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any()) await _identity.Database.MigrateAsync();

            //data seeding for identity will be implemented in the future

            if (!_identity.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "SuperAdmin" });
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });

            }


            if (!_identity.Users.Any())
            {
                var SuperAdmin = new AppUser()
                {
                    UserName = "SuperAdmin",
                    DisplayName = "SuperAdmin",
                    Email = "superadmin@gmail.com",
                    PhoneNumber = "01030245628"

                };
                var admin = new AppUser()
                {
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01030245628"

                };

                await _userManager.CreateAsync(SuperAdmin, "P@ssW0rd");
                await _userManager.CreateAsync(admin, "P@ssW0rd");

                await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
                await _userManager.AddToRoleAsync(admin, "Admin");

            }


        }



    }
}
