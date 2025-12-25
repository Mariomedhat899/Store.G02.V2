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
        public ProductsWithBransAndTypesSpecifications(int? brandId, int? typeId) : base
            (
            P =>
            (!brandId.HasValue || P.BrandId == brandId)
            &&
            (!typeId.HasValue  || P.TypeId == typeId)
            )
        {
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
