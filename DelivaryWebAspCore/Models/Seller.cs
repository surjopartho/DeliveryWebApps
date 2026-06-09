namespace DelivaryWebAspCore.Models
{
    public class Seller
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password  { get; set; }
        public string ShopeName { get; set; }
        public long  Phone { get; set; }

        public List<Product> Products { get; set; }

    }
}
