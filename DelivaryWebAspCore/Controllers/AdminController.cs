using DelivaryWebAspCore.Data;
using DelivaryWebAspCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace DelivaryWebAspCore.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public AdminController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

        }
        public IActionResult Dashboard()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if(userEmail !="partho@gmail.com")
            {
                return Unauthorized();

            }
            var allsellers =_appDbContext.Sellers.ToList();
            ViewBag.Seller = allsellers;
            var allProduct = _appDbContext.Products.Include(p => p.Seller).ToList();


            return View(allProduct);
        }
        [HttpPost]
        public IActionResult UpdateStatus(int ProductId, DeliveryStatus newStatus)
        {
          
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (userEmail != "partho@gmail.com")
            {
                return Unauthorized();
            }
            var product = _appDbContext.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (product != null)
            {
                product.Status = newStatus;
                _appDbContext.Entry(product).Property(x=> x.Status).IsModified = true;
                _appDbContext.SaveChanges();
            }

            return RedirectToAction("Dashboard");
        }
    }
}
