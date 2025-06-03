using Microsoft.Extensions.Caching.Memory;

namespace Asp.net_Web_Api.Meddlewares
{


    public class RateLimiterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;

        public RateLimiterMiddleware(RequestDelegate next, IMemoryCache memoryCache)
        {
            _next = next;
            _memoryCache = memoryCache;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var cacheKey = $"RateLimit_{ip}";

            var requestInfo = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                return new RequestCounter();
            });

            requestInfo!.Count++;


            if (requestInfo.Count > 5)
            {
                context.Response.StatusCode = 429;
                await context.Response
                    .WriteAsync($"Rate limit exceeded, Please wait and try again later.");
                return;

            }

            await _next(context);
        }

        private class RequestCounter
        {
            public int Count { get; set; } = 0;
        }
    }
}


