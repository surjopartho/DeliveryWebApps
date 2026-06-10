using Microsoft.EntityFrameworkCore;

namespace DelivaryWebAspCore.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Seller> Sellers { get; set; }
        public DbSet<Models.Product> Products { get; set; }
        //public DbSet<DelivaryWebAspCore.Models.Product> Products { get; set; }

    }
}
