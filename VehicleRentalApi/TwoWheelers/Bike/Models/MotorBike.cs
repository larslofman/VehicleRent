namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public class MotorBike : IBike
    {
        public MotorBike() { }

        double IBike.GetCost(BikeBooking booking)
        {
            return 1000;
        }
    }
}
