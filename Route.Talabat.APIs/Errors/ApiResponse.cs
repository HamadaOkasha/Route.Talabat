
namespace Route.Talabat.APIs.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode,string? message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            //var message=string.Empty;
            //switch (statusCode)
            //{
            //    case 400:
            //        message = "Bad Request";
            //        break;
            //    case 401:
            //        message = "Unauthorized";
            //        break; 
            //}

            return statusCode switch
            {
                400 => "Bad request",
                401 => "Unauthorized",
                404 => "Resource not found",
                500 => "Server Error",
                _ => null,
            };
        }
    }
}
