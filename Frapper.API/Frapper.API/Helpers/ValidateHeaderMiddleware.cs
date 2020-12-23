using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frapper.API.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValidateHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidateHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            
            if (!httpContext.Request.Path.Value.Contains("Authenticate") && !httpContext.Request.Path.Value.Contains("Swagger"))
            {
                string authorization = httpContext.Request.Headers["Authorization"];
                if (authorization == null)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Access denied Token Missing!");
                    return;
                }

                string versionheader = httpContext.Request.Headers["x-frapper-api-version"];
                if (versionheader == null)
                {
                    httpContext.Response.StatusCode = 401;
                    await httpContext.Response.WriteAsync("Access denied Version Header Missing!");
                    return;
                }

            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ValidateHeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateHeaderMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidateHeaderMiddleware>();
        }
    }
}
