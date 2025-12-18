using AutoMapper;
using Domain.Entites.Products;
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
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>()
                .ForMember(D => D.Type, O => O.MapFrom(S => S.Type.Name))
                .ForMember(D => D.Brand, O => O.MapFrom(S => S.Brand.Name));

            CreateMap<ProductBrand, BrandTypeResponse>();
            CreateMap<ProductType, BrandTypeResponse>();
        }
    }
}
