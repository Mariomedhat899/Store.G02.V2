using Domain.Entites.Products;
using Shared.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.Products
{
    public class ProductsWithBransAndTypesSpecifications : BaseSpecifications<int,Product>
    {
        public ProductsWithBransAndTypesSpecifications(ProductQueryParameters parameters) : base
            (
            P =>
            (!parameters.BrandId.HasValue || P.BrandId == parameters.BrandId)
            &&
            (!parameters.TypeId.HasValue  || P.TypeId == parameters.TypeId)
            &&
            (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search.ToLower()))
           
            )
           
        {
            if (!string.IsNullOrEmpty(parameters.Sorting))
            {
                switch (parameters.Sorting.ToLower())
                {
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;

                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }


            //pageIndex = 3
            //pageSize = 5
            //skip : 2*5 (pageindex -1 ) * pageSize
            //take :5

            ApplyPagingnation(parameters.PageSize, parameters.PageIndex);

            ApplyIncludes();
        }
        public ProductsWithBransAndTypesSpecifications(int id) : base(P => P.Id == id)
        {

            ApplyIncludes();

        }


        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}
