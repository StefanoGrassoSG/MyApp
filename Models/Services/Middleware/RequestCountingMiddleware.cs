
using WebAppCourse.Models.Services.Application;

namespace WebAppCourse.Models.Services.Middleware
{
    public class RequestCountingMiddleware
    {   
        private readonly RequestDelegate _next;
        private readonly RequestCounterService _requestCounterService;

        public RequestCountingMiddleware(RequestDelegate next, RequestCounterService requestCounterService)
        {
            _next = next;
            _requestCounterService = requestCounterService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _requestCounterService.IncrementRequestCount();

            await _next(context);
        }
    }

}