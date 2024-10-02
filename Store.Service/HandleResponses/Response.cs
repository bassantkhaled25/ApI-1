using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.HandleResponses
{
    public class Response

    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public Response(int statusCode, string? message = null) //default)               //constructor

        {
            StatusCode = statusCode;
            Message = message ?? GetStatusCodeMessage(statusCode);       //if msg null => GetStatusCode
        }

        private string GetStatusCodeMessage(int Statuscode) => Statuscode switch

        {


            400 => "Bad Request",
            401 => "You are not autherized!!",
            404 => "Resource not found",
            500 => "Internal Server error",
            _ => "UnKnownStatusCode"
        

        };
    } 
}