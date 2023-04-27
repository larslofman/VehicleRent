using Microsoft.AspNetCore.Mvc;
using VehicleRentalApi.Models;

namespace VehicleRentalApi.Repositories
{
    public interface IVehicleRentalRepo
    {
        Task<IEnumerable<Booking>> GetAllRentals();
        Task<IEnumerable<Vehicle>> GetAllVehicles();
        Task<int> StartVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentStartDistance_km);
        Task<int> EndVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentEndDistance_km);
    }
}
