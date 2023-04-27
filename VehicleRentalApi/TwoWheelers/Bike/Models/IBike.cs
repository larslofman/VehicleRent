namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public interface IBike
    {
        double GetCost(BikeBooking booking);
    }
}