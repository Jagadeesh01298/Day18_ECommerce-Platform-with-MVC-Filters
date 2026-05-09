using ECommerceMvcFilters.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceMvcFilters.Filters
{
    public class RequestResponseLoggingFilter : IAsyncActionFilter
    {
        private readonly IAppLogger _logger;

        public RequestResponseLoggingFilter(IAppLogger logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            string url = context.HttpContext.Request.Path;
            string method = context.HttpContext.Request.Method;

            _logger.LogInfo($"Request Started | Method: {method} | URL: {url}");

            ActionExecutedContext executedContext = await next();

            int statusCode = executedContext.HttpContext.Response.StatusCode;

            _logger.LogInfo($"Request Completed | Method: {method} | URL: {url} | Status Code: {statusCode}");
        }
    }
}
