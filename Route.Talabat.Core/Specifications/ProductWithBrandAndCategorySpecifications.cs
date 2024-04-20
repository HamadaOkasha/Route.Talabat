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
        public ProductWithBrandAndCategorySpecifications(string sort, int? brandId, int? categoryId)
            : base(P =>
                    (!brandId.HasValue || P.BrandId == brandId.Value) &&
                    (!categoryId.HasValue || P.CategoryId == categoryId.Value)
                 )
        {
            AddIncludes();
            //Includes.Add(p => p.Brand);
            //Includes.Add(p => p.Category);
          
            //Sort
            if (!string.IsNullOrEmpty(sort))
            {

                switch (sort)
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
