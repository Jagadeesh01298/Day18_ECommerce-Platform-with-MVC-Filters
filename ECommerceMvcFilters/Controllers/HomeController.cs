using ECommerceMvcFilters.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceMvcFilters.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ErrorViewModel model = new ErrorViewModel
            {
                Message = "Something went wrong.",
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(model);
        }
    }
}
