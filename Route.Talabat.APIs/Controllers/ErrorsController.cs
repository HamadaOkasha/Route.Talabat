using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;

namespace Route.Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]

    [ApiExplorerSettings(IgnoreApi = true)]//no document this api
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code)
        {
            if (code == 401)
                return Unauthorized(new ApiResponse(code));
            else if (code == 404)
                return NotFound(new ApiResponse(code));
            else
                return StatusCode(code);

            //must check code to all  , unauthorized 
            //but there is another middlewares like security modules

        }
    }
}
