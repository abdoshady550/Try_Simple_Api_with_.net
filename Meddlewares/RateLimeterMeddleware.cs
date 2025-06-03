namespace Asp.net_Web_Api.Meddlewares
{
    public class RateLimeterMeddleware
    {
        private readonly RequestDelegate _next;
        private static int _counter = 0;
        private static DateTime _lastRequestDate = DateTime.Now;

        public RateLimeterMeddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            _counter++;
            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _counter = 1;
                _lastRequestDate = DateTime.Now;
                await _next(context);
            }
            else if (_counter > 5)
            {
                _lastRequestDate = DateTime.Now;
                context.Response.StatusCode = 429;

                await context.Response.WriteAsync($"your request limet is exceded {_counter} ");
            }
            else
            {
                _lastRequestDate = DateTime.Now;
                await _next(context);
            }
        }

    }
}
