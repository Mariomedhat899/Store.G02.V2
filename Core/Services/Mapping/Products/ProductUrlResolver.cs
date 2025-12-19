using AutoMapper;
using Domain.Entites.Products;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Products;

namespace Services.Mapping.Products
{
    public class ProductUrlResolver(IConfiguration _configuration) : IValueResolver<Product, ProductResponse, string>
    {
        public string Resolve(Product source, ProductResponse destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}