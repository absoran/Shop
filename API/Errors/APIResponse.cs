using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class APIResponse
    {
        public APIResponse(int statuscode,string msg = null)
        {
            StatusCode = statuscode;
            Message = msg ?? GetMessage(StatusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                404 => "Not Found",
                500 => "Server Error",
                _ => null
            };
        }
    }
}
