using VehicleRentalApi.Models;

namespace VehicleRentalApi.Repositories
{
    public interface IVehicleRentalRepo
    {
        //Task<IEnumerable<VehicleRental>> GetAllRentals();
        IEnumerable<VehicleRental>GetAllRentals();
        void StartVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentStartDistance_km);
        void EndVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentEndDistance_km);
    }
}
