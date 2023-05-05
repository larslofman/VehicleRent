namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public class ManualBike : IBike
    {
        double IBike.GetCost(BikeBooking booking)
        {
            return 200;
        }
    }
}
