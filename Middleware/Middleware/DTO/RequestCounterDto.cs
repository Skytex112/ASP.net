using System;

namespace Middleware.DTO
{
    public class RequestCounterDto
    {
        public int Count { get; set; } = 0;
        public DateTime Start { get; set; } = DateTime.UtcNow;
    }
}