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
        public async Task<IEnumerable<Booking>> GetAllVehicleRentals()
        {
            var rentals = new VehicleRentalRepo().GetAllRentals();
            return rentals;
        }
        [HttpPost("StartVehicleRent")]
        public async Task<IActionResult> StartVehicleRent(string registrationNumber, string personalIdNumber, int rentStartDistance_km)
        {
             var rows = await _vehicleRentalRepo.StartVehicleRent(registrationNumber.ToUpper(), personalIdNumber, rentStartDistance_km);
                
            if(rows > 0)
                return Ok(rows);
            return BadRequest($"Cannot rent the car with registration number {registrationNumber}");
        }
        [HttpPost("EndVehicleRent")]
        public async Task<IActionResult> EndVehicleRent(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            _vehicleRentalRepo.EndVehicleRent(registrationNumber.ToUpper(), personalIdNumber, rentEndDistance_km);
            return Ok();
        }
    }
}
