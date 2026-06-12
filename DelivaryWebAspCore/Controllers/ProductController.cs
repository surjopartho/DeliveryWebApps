using DelivaryWebAspCore.Data;using DelivaryWebAspCore.Models;using DelivaryWebAspCore.Services;using Microsoft.AspNetCore.Mvc;using System.Runtime.InteropServices;namespace DelivaryWebAspCore.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly DeliveryService _deliveryService;

        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _deliveryService = new DeliveryService();
        }

        public IActionResult Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "SellerName")?.Value;
            var products = _appDbContext.Products.Where(p =>p.UserId== userId).ToList();
            return View(products);

        }

        public IActionResult Edit(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "SellerName")?.Value;
            var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "SellerName")?.Value;
            var existing = _appDbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);

            if (existing == null) return NotFound();

            existing.CustomerName = product.CustomerName;
            existing.ProductName = product.ProductName;
            existing.CustomerLocation = product.CustomerLocation;
            existing.Weight = product.Weight;
            existing.IsType = product.IsType;

            existing.Charge = _deliveryService.CalculateCharge(product.Weight);

            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "SellerName")?.Value;
            var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null) return NotFound();

            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }




        [HttpPost]
        public IActionResult Index(Product product)
        {
            ModelState.Remove("UserId");
            ModelState.Remove("Seller");
            ModelState.Remove("CustomerName");
            ModelState.Remove("ProductPrice");
            //ModelState.Remove("Delivaryboy");
            var userId = User.Claims.FirstOrDefault(c => c.Type == "SellerName")?.Value;
            product.UserId = userId;

            var currentSeller = _appDbContext.Sellers.FirstOrDefault(s => s.Name == userId);
            if (currentSeller != null)
            {
                product.SellerId = currentSeller.Id;
            }
            else
            {



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
            }

            var defaultSellerName = _appDbContext.Sellers.FirstOrDefault();

            if (string.IsNullOrEmpty(product.CustomerName)) product.CustomerName = defaultSellerName.Name;
            double charge = _deliveryService.CalculateCharge(product.Weight);

            
            ViewBag.Charge = charge;
            product.Charge = charge;

            if (ModelState.IsValid)
            {
                _appDbContext.Products.Add(product);
                _appDbContext.SaveChanges();

                return RedirectToAction("Index");
            }


            var products = _appDbContext.Products.Where(p => p.UserId == userId).ToList();

            return View(products);
        }
    }
}