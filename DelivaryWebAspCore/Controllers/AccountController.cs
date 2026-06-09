using Microsoft.AspNetCore.Mvc;
using DelivaryWebAspCore.Models.ViewModels;

namespace DelivaryWebAspCore.Controllers
{
    public class AccountController : Controller
    {
        // GET: Login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // POST: Login form submit
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.Email == "admin@gmail.com" && model.Password == "1234")
            {
                return RedirectToAction("Index", "Product");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View(model);
        }
    }
}