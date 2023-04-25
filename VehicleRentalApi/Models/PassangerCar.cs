namespace VehicleRentalApi.Models
{
    public class PassengerCar : Vehicle, IVehicleRental
    {
        public float GetPrice()
        {
            return 10;
        }
    }
}
