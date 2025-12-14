using Domain.Contracts;
using Domain.Entites.Products;
using Microsoft.EntityFrameworkCore;
using Periestence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Periestence
{
    public class DbInitializer(StoreDbContext _context) : IDbInitializer
    {
        public async Task InitializeAsync()
        {
            if( _context.Database.GetPendingMigrationsAsync().GetAwaiter().GetResult().Any())await _context.Database.MigrateAsync();


          if(!await _context.ProductBrands.AnyAsync())
            {
                var brandData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands is not null && brands.Count > 0) await _context.ProductBrands.AddRangeAsync(brands);
                

            }

          if(!await _context.ProductTypes.AnyAsync())
            {
                var TypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\types.json");

                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types is not null && Types.Count > 0) await _context.ProductTypes.AddRangeAsync(Types);
                

            }

          if(!await _context.Products.AnyAsync())
            {
                var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Periestence\Data\DataSeeding\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (products is not null && products.Count > 0) await _context.Products.AddRangeAsync(products);

            }
            
            await _context.SaveChangesAsync();


        }
    }
}
