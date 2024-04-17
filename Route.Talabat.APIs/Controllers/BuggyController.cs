using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.Infrastructure.Data;

namespace Route.Talabat.APIs.Controllers
{
    //this to help us 
    public class BuggyController : BaseApiController
    {
        private readonly ApplicationDbContext _dbContext;

        public BuggyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //404
        [HttpGet("notFound")] //api/buggy/notFound
        public ActionResult GetNotFoundError()
        {
            var product = _dbContext.Products.Find(100);
            if (product is null) 
                //return NotFound();
                //return NotFound(new { StatusCode=404,Message="Not Found"});
                return NotFound(new ApiResponse(404));
            return Ok(product);
        }
        //500
        [HttpGet("servererror")]//api/buggy/servererror
        //null ref exception
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);
            var productToReturn = product.ToString();//will throw exception
            return Ok(productToReturn);

            //return 500 and message = server error
        }
        //400
        [HttpGet("badrequest")]//api/buggy/badrequest
        public ActionResult GetBadRequest()
        {
          //return BadRequest();
          return BadRequest(new ApiResponse(400));
        }
        //400 with errors of type dictionary
        //when debug will not go to here he want an int not string  
        [HttpGet("badrequest/{id}")]//api/buggy/badrequest/five
        public ActionResult GetBadRequest(int id) //validation Error
        {
            return Ok();
        }
        //401
        [HttpGet("unauthorized")]//api/buggy/unauthorized
        public ActionResult GetUnAuthorizedError()
        {
            return Unauthorized(new ApiResponse(401));
        }
        //and not found by no api
    }


    ///3 like themselvees
    ///GetNotFoundError
    ///GetBadRequest
    ///GetUnAuthorizedError

    //GetBadRequest(int id)

    //GetServerError
    
    //with no this name /hamda
}
