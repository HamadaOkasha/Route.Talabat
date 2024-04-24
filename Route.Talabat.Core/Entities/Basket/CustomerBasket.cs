using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route.Talabat.Core.Entities.Basket
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
            Items=new List<BasketItem>(); // return 0 to araba
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
