using ECommerceMvcFilters.Models;
using ECommerceMvcFilters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceMvcFilters.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly IAppLogger _logger;

        public GlobalExceptionFilter(IAppLogger logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Unhandled exception occurred.", context.Exception);

            ErrorViewModel errorModel = new ErrorViewModel
            {
                Message = "Something went wrong. Please try again later.",
                RequestId = context.HttpContext.TraceIdentifier
            };

            context.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorViewModel>(
                    new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                    context.ModelState)
                {
                    Model = errorModel
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
