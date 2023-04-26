namespace VehicleRentalApi.Models
{

    //public interface IVehicle
    //{
    //    double GetCost();
    //}

    public class Vehicle //:IVehicle
    {
        public string RegistrationNumber { get; set; }
        public string Category { get; set; }
        public double Distance_km { get; set; }
        public double CargoSpace_m2 { get; set; }


        public virtual double GetCost(Category category, Booking booking)
        {
            return 0;
        }
    }
}
