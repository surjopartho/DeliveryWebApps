using DelivaryWebAspCore.Models;  
using Microsoft.EntityFrameworkCore;

namespace DelivaryWebAspCore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}