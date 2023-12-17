using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProductsInventory.Domain.Middlewares
{
    public enum ErrorType
    {
        GeneralError,
        NotFound,
        // Add more error types as needed
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                //await HandleExceptionAsync(context, ex);
            }
        }

        //private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        //{
        //    var code = HttpStatusCode.InternalServerError; // Default to 500 if unexpected

            // Map exceptions to specific error types
            //var errorType = MapErrorType(exception);

            //switch (errorType)
            //{
            //    case ErrorType.NotFound:
            //        code = HttpStatusCode.NotFound;
            //        break;
            //    // Add more cases for other error types
            //    default:
            //        break;
            //}

            //var result = JsonConvert.SerializeObject(new { error = errorType.ToString(), message = exception.Message });
            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)code;
            //return context.Response.WriteAsync(result);
        //}

        //private static ErrorType MapErrorType(Exception exception)
        //{
        //    // Map specific exceptions to error types
        //    if (exception is NotFoundException) return ErrorType.NotFound;
        //    // Add more mappings as needed

        //    // Default to GeneralError
        //    return ErrorType.GeneralError;
        //}
    }

}
