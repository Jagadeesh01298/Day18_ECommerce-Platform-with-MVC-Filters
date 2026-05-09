using ECommerceMvcFilters.Filters;
using ECommerceMvcFilters.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvcFilters.Controllers
{
    public class ProductController : Controller
    {
        private static readonly List<Product> Products = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Laptop",
                Price = 55000,
                Description = "High performance laptop"
            },
            new Product
            {
                Id = 2,
                Name = "Mobile Phone",
                Price = 25000,
                Description = "Android smartphone"
            },
            new Product
            {
                Id = 3,
                Name = "Headphones",
                Price = 2000,
                Description = "Wireless headphones"
            }
        };

        public IActionResult Index()
        {
            return View(Products);
        }

        public IActionResult Details(int id)
        {
            Product? product = Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ServiceFilter(typeof(CustomAuthenticationFilter))]
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            product.Id = Products.Max(p => p.Id) + 1;

            Products.Add(product);

            return RedirectToAction("Index");
        }

        public IActionResult ThrowError()
        {
            throw new Exception("This is a test exception from ProductController.");
        }

        public static Product? GetProductById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
