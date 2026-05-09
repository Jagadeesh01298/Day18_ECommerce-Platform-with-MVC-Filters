using ECommerceMvcFilters.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvcFilters.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Username == "admin" && model.Password == "password")
            {
                HttpContext.Session.SetString("Username", model.Username);

                return RedirectToAction("Index", "Product");
            }

            ModelState.AddModelError("", "Invalid username or password.");

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");

            return RedirectToAction("Login", "Account");
        }
    }
}
