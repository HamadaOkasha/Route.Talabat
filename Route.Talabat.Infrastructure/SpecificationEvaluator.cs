using Microsoft.EntityFrameworkCore;
using Route.Talabat.Core.Entities;
using Route.Talabat.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Infrastructure
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // IQueryable to return query not data
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery; // _dbContext.Set<TEntity>()

            if (spec.Criteria is not null) // P => P.Id == id
                query = query.Where(spec.Criteria);


            //TEntity ---> Product
            /// query = _dbContext.Set<TEntity>();
            /// query = _dbContext.Set<TEntity>().Where(P => P.Id == id);
            /// includes
            /// 1. P => P.Brand
            /// 2. P => P.Category

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            /// _dbContext.Set<TEntity>().Where(P => P.Id == id).Include(P => P.Brand);
            /// _dbContext.Set<TEntity>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category);


            return query;
        }
    }
}
