using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace KinoPoiskApp.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now}] Запит: {context.Request.Method} {context.Request.Path}");
            await _next(context);
            Console.WriteLine($"[{DateTime.Now}] Відповідь: {context.Response.StatusCode}");
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}