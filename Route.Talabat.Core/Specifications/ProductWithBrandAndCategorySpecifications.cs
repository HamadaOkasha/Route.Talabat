using Route.Talabat.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Specifications
{
    public class ProductWithBrandAndCategorySpecifications : BaseSpecification<Product>
    {
        //this ctor will be used for creating an object , that will be for build the query that will get all Products
        public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
            : base(P =>
                    (string.IsNullOrEmpty(specParams.Search) ||P.Name.ToLower().Contains(specParams.Search)) &&
                    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) &&
                    (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
                 )
        {
            AddIncludes();
            //Includes.Add(p => p.Brand);
            //Includes.Add(p => p.Category);
          
            //Sort
            if (!string.IsNullOrEmpty(specParams.Sort))
            {

                switch (specParams.Sort)
                {
                    case "priceAsc":
                        //OrderBy = p => p.Price;
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }

            }
            else
                AddOrderBy(p => p.Name);

            //Paginatiom
            //TotalProducts= 18 ~ 20
            //PageSize      = 5
            //PageIndex     = 3

            //Skip 10 , take 5
            ApplayPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);//Skip , Take

            ///SQL -> Where then OrderBy then Top
            ///--- -> Filter then Sort then Pagination
        }


        //this ctor will be used for creating an object , that will be for build the query that will get sprcific Products
        public ProductWithBrandAndCategorySpecifications(int id)
         : base(P => P.Id == id) //second ctor
        {
            AddIncludes();
            // Includes.Add(p => p.Brand);
            // Includes.Add(p => p.Category);
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}
