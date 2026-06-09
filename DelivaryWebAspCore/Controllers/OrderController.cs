using Microsoft.AspNetCore.Mvc;

namespace DelivaryWebAspCore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
