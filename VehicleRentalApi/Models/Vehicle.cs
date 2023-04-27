namespace VehicleRentalApi.Models
{
    public class Vehicle
    {
        public string RegistrationNumber { get; set; }
        public string CategoryCode { get; set; }
        public double Distance_km { get; set; }

        public virtual double GetCost(Category category, Booking booking)
        {
            return 0;
        }
    }
}
