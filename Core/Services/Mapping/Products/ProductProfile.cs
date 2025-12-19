using AutoMapper;
using Domain.Entites.Products;
using Microsoft.Extensions.Configuration;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Services.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration _configuration)
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))
                .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name))
                //.ForMember(D => D.PictureUrl, O => O.MapFrom(S => $"{_configuration["BaseUrl"]}{ S.PictureUrl }"));
                .ForMember(D => D.PictureUrl, O => O.MapFrom(new ProductUrlResolver(_configuration)));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
