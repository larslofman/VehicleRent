namespace VehicleRentalApi.Models
{
    public interface IVehicleRental
    {
        float GetPrice();
    }
    public class Lorry : Vehicle, IVehicleRental
    {
        float CargoSpace { get; }
        public float GetPrice()
        {
            return 20;
        }
     }
}
