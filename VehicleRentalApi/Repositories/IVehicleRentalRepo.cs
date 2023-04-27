using Microsoft.AspNetCore.Mvc;
using VehicleRentalApi.Models;

namespace VehicleRentalApi.Repositories
{
    public interface IVehicleRentalRepo
    {
        IEnumerable<Booking> GetAllRentals();
        Task<int> StartVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentStartDistance_km);
        Task<int> EndVehicleRent(string RegistrationNumber, string PersonalIdNumber, int rentEndDistance_km);
    }
}
