namespace ECommerceMvcFilters.Services
{
    public interface IAuthService
    {
        bool IsUserLoggedIn();

        string? GetLoggedInUsername();
    }
}
