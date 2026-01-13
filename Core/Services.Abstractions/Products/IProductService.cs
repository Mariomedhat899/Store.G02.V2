using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.Products
{
    public interface IProductService
    {
     Task<IEnumerable<ProductResponse>>   GetAllProductsAsync(int? brandId,int? typeId, string? search, string? sort, int? PageIndex, int? PageSize);
     Task<ProductResponse>   GetProductByIdAsync(int id);
     Task<IEnumerable<BrandTypeResponse>>   GetAllBrandsAsync();
     Task<IEnumerable<BrandTypeResponse>>   GetAllTypesAsync();
    }
}
