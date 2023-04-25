namespace VehicleRentalApi.Models
{
    public class Combi : Vehicle, IVehicleRental
    {
        public float GetPrice()
        {
            return 15;
        }
    }

}
