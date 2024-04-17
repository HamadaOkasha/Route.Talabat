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
        public ProductWithBrandAndCategorySpecifications()
            : base()
        {
              AddIncludes();
           //Includes.Add(p => p.Brand);
           //Includes.Add(p => p.Category);
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
