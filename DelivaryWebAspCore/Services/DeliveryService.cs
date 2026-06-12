namespace DelivaryWebAspCore.Services
{
    public class DeliveryService
    {
        private const double ratePerKg = 20;
        public double CalculateCharge (double Weight)
        {
            return Weight * ratePerKg;
        }


    }
}
