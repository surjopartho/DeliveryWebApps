using DelivaryWebAspCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SellerController : Controller
{
    private readonly AppDbContext _context;

    public SellerController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var sellers = _context.Sellers.ToList();
        return View(sellers);
    }

    public IActionResult Details(int id)
    {
        var seller = _context.Sellers
            .Include(s => s.Products)
            .FirstOrDefault(s => s.Id == id);

        return View(seller);
    }
}