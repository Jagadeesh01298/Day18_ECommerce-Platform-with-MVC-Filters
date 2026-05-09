using ECommerceMvcFilters.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ECommerceMvcFilters.Filters
{
    public class CustomAuthenticationFilter : IAsyncAuthorizationFilter
    {
        private readonly IAuthService _authService;
        private readonly IAppLogger _logger;

        public CustomAuthenticationFilter(IAuthService authService, IAppLogger logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!_authService.IsUserLoggedIn())
            {
                string url = context.HttpContext.Request.Path;

                _logger.LogInfo($"Unauthorized access attempt | URL: {url}");

                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            return Task.CompletedTask;
        }
    }
}
