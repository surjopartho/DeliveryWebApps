using DelivaryWebAspCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DelivaryWebAspCore.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>();

        public IActionResult Index()
        {
            return View(products);
        }

        [HttpPost]
        public IActionResult Index(Product product)
        {
            ModelState.Remove("Seller");

            if (ModelState.IsValid)
            {
                product.ProductId = products.Count + 1;
                product.SellerId = 1;

                products.Add(product);

                return RedirectToAction("Index");
            }

            return View(products);
        }
    }
}