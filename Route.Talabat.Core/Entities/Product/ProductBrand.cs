using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Product
{
    public class ProductBrand: BaseEntity
    {
        public string Name { get; set; }
       // public ICollection<Product> Products { get; set; } = new HashSet<Product>();
        //one-2-one if not write or not write in configuration
    }
}
