using Microsoft.AspNetCore.Http;
using Middleware.DTO;
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Threading.Tasks;

namespace Middleware.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ConcurrentDictionary<string, RequestCounterDto> _requests
            = new ConcurrentDictionary<string, RequestCounterDto>();

        private const int LIMIT = 10;
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(1);

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var counter = _requests.GetOrAdd(clientIp, _ => new RequestCounterDto());

            if (DateTime.UtcNow - counter.Start >= WINDOW)
            {
                counter.Count = 0;
                counter.Start = DateTime.UtcNow;
            }

            counter.Count++;
            if (counter.Count > LIMIT)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsJsonAsync(new
                {
                    error = "Too Many Requests",
                    message = "Ви перевищили ліміт у 10 запитів за хвилину."
                });
                return;
            }

            await _next(context);
        }
    }
}