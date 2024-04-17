using Route.Talabat.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs.Middlewares
{
    /*By Convension Base - factory Base [implement IMiddleware] - speedly*/

    //1-By Convension
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next /*reference to next middleware */, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                //if i want take an action with the request 

                await _next.Invoke(httpContext);//go To next Middleware

                //if i want take an action with the response 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);          //development Env
                /// log exception in (Database | Files)  //Production Env

                //httpContext.Response.StatusCode = 500;
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    :
                    new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);


                //to return to front as camelCase
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // var json = JsonSerializer.Serialize(response);
                var json = JsonSerializer.Serialize(response, options);


                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    ////factory Base [implement IMiddleware]
    //public class ExceptionMiddleware : IMiddleware
    //{
    //    private readonly ILogger<ExceptionMiddleware> _logger;
    //    private readonly IWebHostEnvironment _env;

    //    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    //    {
    //        _logger = logger;
    //        _env = env;
    //    }

    //    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
    //    {
    //        try
    //        {

    //            //if i want take an action with the request 

    //            await _next.Invoke(httpContext);//go To next Middleware

    //            //if i want take an action with the response 
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex.Message);          //development Env
    //            /// log exception in (Database | Files)  //Production Env

    //            //httpContext.Response.StatusCode = 500;
    //            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //            httpContext.Response.ContentType = "application/json";

    //            var response = _env.IsDevelopment() ?
    //                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
    //                :
    //                new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
    //            //to return to front as camelCase
    //            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    //            var json = JsonSerializer.Serialize(response);
    //            //   var json = JsonSerializer.Serialize(response, options);
    //            await httpContext.Response.WriteAsync(json);
    //        }
    //    }
  
    //}
}

