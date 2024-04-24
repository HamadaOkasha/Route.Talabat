using Route.Talabat.Core.Entities.Basket;
using System.ComponentModel.DataAnnotations;

namespace Route.Talabat.APIs.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
