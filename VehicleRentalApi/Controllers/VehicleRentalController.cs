using Microsoft.AspNetCore.Mvc;
using VehicleRentalApi.Repositories;
using VehicleRentalApi.Models;

namespace VehicleRenting.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VehicleRentalController : ControllerBase
    {
        private readonly IVehicleRentalRepo _vehicleRentalRepo;

        public VehicleRentalController(IVehicleRentalRepo vehicleRentalRepo)
        {
            _vehicleRentalRepo = vehicleRentalRepo;
        }

        [HttpGet("GetAllVehicleRentals")]
        public IEnumerable<Booking> GetAllVehicleRentals()
        {
            var rentals = new VehicleRentalRepo().GetAllRentals();
            return rentals;
        }
        [HttpPost("StartVehicleRent")]
        public void StartVehicleRent(string registrationNumber, string personalIdNumber, int rentStartDistance_km)
        {
            _vehicleRentalRepo.StartVehicleRent(registrationNumber, personalIdNumber, rentStartDistance_km);
        }
        [HttpPost("EndVehicleRent")]
        public void EndVehicleRent(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            _vehicleRentalRepo.EndVehicleRent(registrationNumber, personalIdNumber, rentEndDistance_km);
        }
    }
}
