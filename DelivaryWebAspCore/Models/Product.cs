using System.ComponentModel.DataAnnotations;

namespace DelivaryWebAspCore.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(150)]
        public string ProductName { get; set; }

        [Range(1, 10000)]
        public double Weight { get; set; }

        [Required]
        public TypeOfProduct IsType { get; set; }

        public int SellerId { get; set; }

        public Seller? Seller { get; set; }

        public double Charge { get; set; }

        public double ProductPrice { get; set; }

        [Required]
        [MaxLength(150)]
        public string CustomerLocation { get; set; }
        public string? UserId { get; set; }

        public DeliveryStatus Status { get; set; } = DeliveryStatus.Pending;
    }

    public enum TypeOfProduct
    {
        Electronics,
        Clothing,
        Food,
        Furniture,
        Books,
        Toys,
        BeautyProducts,
        SportsEquipment,
        AutomotiveParts,
        HealthProducts
    }

    public enum DeliveryStatus
    {
        Pending,
        Processing,
        OnTheWay,
        Delivered,
        Cancelled
    }

}