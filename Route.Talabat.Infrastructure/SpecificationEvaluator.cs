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
            //TEntity ---> Product


            //Where
            if (spec.Criteria is not null) // P => P.Id == id , && P=>P.BrandId=10 && P=>P.CategoryId=10
                query = query.Where(spec.Criteria);
            
            ///where
            /// query = _dbContext.Set<Product>();
            /// query = _dbContext.Set<Product>().Where(P => P.Id == id);


            //Sort
            if (spec.OrderBy is not null)//p=>p.Name
            {
                query = query.OrderBy(spec.OrderBy);
            }
            else if (spec.OrderByDesc is not null)//p=>p.Price
            {
                query = query.OrderBy(spec.OrderByDesc);
            }

            ///Sort
            ///query = _dbContext.Set<Product>().OrderBy(p=>p.Nmae);
            

            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            /// includes
            /// 1. P => P.Brand
            /// 2. P => P.Category
            /// _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand);
            /// _dbContext.Set<Product>().Where(P => P.Id == id).Include(P => P.Brand).Include(P => P.Category);
            /// 
            /// _dbContext.Set<Product>().OrderBy(p=>p.Nmae).Include(P => P.Brand);
            /// _dbContext.Set<Product>().OrderBy(p=>p.Nmae).Include(P => P.Brand).Include(P => P.Category);


            return query;
        }
    }
}
