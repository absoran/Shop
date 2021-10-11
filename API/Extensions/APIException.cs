using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;

namespace API.Extensions
{
    public class APIException : APIResponse
    {
        public APIException(int statusCode,string message = null,string details = null) : base(statusCode,message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
