using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Core.Specifications;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo,IMapper mapper)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            //  var products = await _productsRepo.GetAllAsync();

           // var spec = new BaseSpecification<Product>();
           
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await _productsRepo.GetAllWithSpecAsync(spec);

            // JsonResult result = new JsonResult(products);
            // result.StatusCode=200;
            // return result;

            // OkObjectResult result = new OkObjectResult(products);
            // return result;

            //  return Ok(products);
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));//200

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int? id)
        {
            if (!id.HasValue)
                return BadRequest();//400

            //var product = await _productsRepo.GetAsync(id.Value);
            var spec = new ProductWithBrandAndCategorySpecifications(id.Value);
            var product = await _productsRepo.GetWithSpecAsync(spec);

            if (product is null)
                return NotFound(new ApiResponse(404));
                // return NotFound(new {Message="Not Found" ,StatusCode= 404});//404
               // return NotFound();//404

           // return Ok(product);//200
            return Ok(_mapper.Map<Product,ProductToReturnDto>(product));//200
        }
    }
}
