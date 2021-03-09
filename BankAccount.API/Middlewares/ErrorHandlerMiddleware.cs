using BankAccount.API.CustomException;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BankAccount.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        #region ctor and props
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        } 
        #endregion
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch(Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
              var error =  new CustomeServerErrorMsg(DateTime.UtcNow, "Internal server error");
                string jsonString = JsonSerializer.Serialize(error);
                _logger.LogError($"Reference Number {error.ReferenceNumber}");
                _logger.LogError(ex.Message);
                await httpContext.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
          
        }

      
    }
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
