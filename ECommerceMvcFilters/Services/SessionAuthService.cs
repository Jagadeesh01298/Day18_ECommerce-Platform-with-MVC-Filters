namespace ECommerceMvcFilters.Services
{
    public class SessionAuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionAuthService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool IsUserLoggedIn()
        {
            string? username = _httpContextAccessor.HttpContext?.Session.GetString("Username");

            return !string.IsNullOrEmpty(username);
        }

        public string? GetLoggedInUsername()
        {
            return _httpContextAccessor.HttpContext?.Session.GetString("Username");
        }
    }
}
