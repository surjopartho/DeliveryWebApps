using Microsoft.AspNetCore.Mvc;
using DelivaryWebAspCore.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using DelivaryWebAspCore.Data;
using Microsoft.AspNetCore.Identity;
using DelivaryWebAspCore.Models;
namespace DelivaryWebAspCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly PasswordHasher<Seller> _passwordHasher;

        public AccountController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _passwordHasher = new PasswordHasher<Seller>();

        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);
                if (userEmail == "partho@gmail.com")
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return RedirectToAction("Index", "Product");

            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var seller = _appDbContext.Sellers.FirstOrDefault(s => s.Email == model.Email);

            if (seller != null)
            {
                
                var result = _passwordHasher.VerifyHashedPassword(seller, seller.Password, model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, seller.Name),
                        new Claim(ClaimTypes.Email, seller.Email),
                        new Claim("SellerName", seller.Name)
                    };

                    if (seller.Email == "partho@gmail.com")
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    
                    
                    if (model.Email == "partho@gmail.com")
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }

                    return RedirectToAction("Index", "Product");
                }
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var sellerExists = _appDbContext.Sellers.Any(u => u.Email == model.Email);
            if (sellerExists)
            {
                ModelState.AddModelError("Email", "Email is already registered.");
                return View(model);
            }

            

            var newSeller = new Seller
            {
                Name = model.Name,
                Email = model.Email,
                ShopeName = model.ShopeName,
                Phone = model.Phone,
            };
            newSeller.Password = _passwordHasher.HashPassword(newSeller, model.Password);

            _appDbContext.Sellers.Add(newSeller);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Login", "Account");

        }

        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");

        }
    }
}