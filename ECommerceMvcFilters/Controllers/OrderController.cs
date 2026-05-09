using ECommerceMvcFilters.Filters;
using ECommerceMvcFilters.Models;
using ECommerceMvcFilters.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvcFilters.Controllers
{
    [ServiceFilter(typeof(CustomAuthenticationFilter))]
    public class OrderController : Controller
    {
        private readonly IAuthService _authService;

        private static readonly List<Order> Orders = new List<Order>();

        public OrderController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            string username = _authService.GetLoggedInUsername() ?? string.Empty;

            List<Order> userOrders = Orders
                .Where(o => o.Username == username)
                .ToList();

            return View(userOrders);
        }

        public IActionResult PlaceOrder(int productId)
        {
            Product? product = ProductController.GetProductById(productId);

            if (product == null)
            {
                return NotFound();
            }

            string username = _authService.GetLoggedInUsername() ?? "Unknown";

            Order order = new Order
            {
                Id = Orders.Count + 1,
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Username = username,
                OrderDate = DateTime.Now
            };

            Orders.Add(order);

            return RedirectToAction("Index");
        }
    }
}
