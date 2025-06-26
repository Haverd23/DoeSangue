using DOS.Core.Exceptions.DOS.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace DOS.Core.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AppException ex)
            {
                httpContext.Response.StatusCode = ex.StatusCode;
                httpContext.Response.ContentType = "text/plain";  

                await httpContext.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new { message = "Erro interno no servidor", details = ex.Message });
                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}