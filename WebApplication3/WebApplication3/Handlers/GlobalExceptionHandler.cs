using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Exceptions;
using System.Diagnostics;

namespace WebApplication3.Handlers
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                ArgumentException => StatusCodes.Status400BadRequest,
                UnauthorizedAppException => StatusCodes.Status401Unauthorized,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                ConflictException => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(statusCode),
                Type = $"https://httpstatuses.com/{statusCode}",
                Instance = httpContext.Request.Path,
                Detail = exception.Message
            };

            problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static string GetTitle(int statusCode) => statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            404 => "Not Found",
            409 => "Conflict",
            _ => "Internal Server Error"
        };
    }
}