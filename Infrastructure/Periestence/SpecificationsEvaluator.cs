using Domain.Contracts;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Services.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Periestence
{
    public static class SpecificationsEvaluator
    {

        public static IQueryable<TEntity> GetQuery<TKey,TEntity>(IQueryable<TEntity> InputQuery,ISpecifications<TKey,TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = InputQuery;
            if(spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }

            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
           query = spec.Includes.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));


            return query;

        }
    }
}
