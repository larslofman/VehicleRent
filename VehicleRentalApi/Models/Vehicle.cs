namespace VehicleRentalApi.Models
{
    public abstract class Vehicle 
    {
        protected string RegistrationNumber { get; set; }
        protected string Category { get; set; }
        protected float Distance_km { get; set; }
     }
}
