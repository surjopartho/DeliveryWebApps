namespace DelivaryWebAspCore.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        public string SellerName { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public double Weight { get; set; }
        public string Delivaryboy { get; set; }
        public DeliveryStatus IsDelivary { get; set; }

        public int SellerId { get; set; }

        public Seller Seller { get; set; }


    }

    public enum DeliveryStatus
    {
        Pending,
        InProgress,
        Delivered,
        Cancelled
    }
}
