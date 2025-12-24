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
        public ProductsWithBransAndTypesSpecifications(Expression<Func<Product, bool>>? expression) : base(expression)
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);

        }
    }
}
