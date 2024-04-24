using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Basket;
using Route.Talabat.Core.IRepositories;

namespace Route.Talabat.APIs.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;

        public BasketController(
            IMapper mapper,
            IBasketRepository basketRepository)
        {
            _mapper = mapper;
            _basketRepository = basketRepository;
        }

        [HttpGet]//[HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBaket(string id)
        {
            var  basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket??new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var createdOrUpdated=await  _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdated is null) return BadRequest(new ApiResponse(400));
           
            return Ok(createdOrUpdated);

        }
        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
