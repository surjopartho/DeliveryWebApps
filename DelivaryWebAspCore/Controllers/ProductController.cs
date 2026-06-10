using DelivaryWebAspCore.Data;
using DelivaryWebAspCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace DelivaryWebAspCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult Index(Product product)
        {
            ModelState.Remove("Seller");
            ModelState.Remove("SellerName");
            ModelState.Remove("Delivaryboy");

            var defaultSeller = _appDbContext.Sellers.FirstOrDefault();
            if (defaultSeller == null)
            {
                defaultSeller = new Seller
                {
                    Name = "Default Seller",
                    Email = "Seller@gmail.com",
                    Password = "123456",
                    ShopeName = "Default Shop",
                    Phone = 1234567890
                };
                _appDbContext.Sellers.Add(defaultSeller);
                _appDbContext.SaveChanges();
            }

            product.SellerId = defaultSeller.Id;

            if (string.IsNullOrEmpty(product.SellerName)) product.SellerName = defaultSeller.Name;
            if (string.IsNullOrEmpty(product.Delivaryboy)) product.Delivaryboy = "Not Assigned";

            if (ModelState.IsValid)
            {
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            var products = _appDbContext.Products.ToList();
            return View(products);
        }
    }
}