using Domain.Entites.Products;
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
        public ProductsWithBransAndTypesSpecifications(int? brandId, int? typeId,string? sort, int? PageIndex, int? PageSize) : base
            (
            P =>
            (!brandId.HasValue || P.BrandId == brandId)
            &&
            (!typeId.HasValue  || P.TypeId == typeId)
            &&
            (string.IsNullOrEmpty(search) || P.Name.ToLower().Contains(search.ToLower()))
            )
           
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
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

            ApplyPagingnation(PageSize.Value, PageIndex.Value);

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
