using ECommerceMvcFilters.Filters;
using ECommerceMvcFilters.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.AddScoped<IAppLogger, FileAppLogger>();
builder.Services.AddScoped<IAuthService, SessionAuthService>();

builder.Services.AddScoped<RequestResponseLoggingFilter>();
builder.Services.AddScoped<CustomAuthenticationFilter>();
builder.Services.AddScoped<GlobalExceptionFilter>();

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.AddService<RequestResponseLoggingFilter>();
    options.Filters.AddService<GlobalExceptionFilter>();
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
