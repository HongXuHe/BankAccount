using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount.API.Middlewares
{
    //fake auth, only used to check http header username is exist
    public class FackAuthMiddleware
    {
        #region ctor and props
        private readonly RequestDelegate _next;
        private readonly ILogger<FackAuthMiddleware> _logger;

        public FackAuthMiddleware(RequestDelegate next, ILogger<FackAuthMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        #endregion

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {


                var userName = httpContext.Request.Headers.SingleOrDefault(x => x.Key == "Username").Value
                    .FirstOrDefault();
                if (userName == null)
                {
                    httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                    httpContext.Response.ContentType = "application/json";
                    await httpContext.Response.WriteAsync("Authorization Failed");
                }
                else
                {
                    //get controller, action name
                    var controllerActionDescriptor = httpContext
                        .GetEndpoint()
                        .Metadata
                        .GetMetadata<ControllerActionDescriptor>();

                    var controllerName = controllerActionDescriptor.ControllerName;
                    var actionName = controllerActionDescriptor.ActionName;

                    //if is create user, and UserName is not Admin will unauthorize
                    if (controllerName == "User" && actionName == "AddUser" && userName != "Admin")
                    {
                        httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                        httpContext.Response.ContentType = "application/json";
                        await httpContext.Response.WriteAsync("Authorization Failed");
                    }
                    else
                    {
                        await _next.Invoke(httpContext);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message,ex);
                throw;
            }
        }

    }

    //extension method
    public static class FackAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseFakeAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FackAuthMiddleware>();
        }
    }
}
