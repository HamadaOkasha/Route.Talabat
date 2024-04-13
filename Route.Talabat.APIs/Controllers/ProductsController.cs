using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;

namespace Route.Talabat.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;

        public ProductsController(IGenericRepository<Product> productsRepo)
        {
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productsRepo.GetAllAsync();

            // JsonResult result = new JsonResult(products);
            // result.StatusCode=200;
            // return result;
            
            // OkObjectResult result = new OkObjectResult(products);
            // return result;

            return Ok(products);
        }
    }
}
