using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Product
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }

        // [ForeignKey(nameof(Product.Brand))]
        // public int ProductBrandId { get; set; }//FK for ProductBrand Entity

        // [ForeignKey(nameof(Product.Brand))]  //-> write it wiht fluent api better
        public int BrandId { get; set; }//FK for ProductBrand Entity
        
        //[InverseProperty(nameof(ProductBrand.Products))]// if more than one relationship
        public ProductBrand Brand { get; set; }//Navigtional property [one]


        // [ForeignKey(nameof(Product.Category))]  //-> write it wiht fluent api better
        public int CategoryId { get; set; }//FK for ProductCategory Entity
        public ProductCategory Category { get; set; }//Navigtional property [one]
    }
}
