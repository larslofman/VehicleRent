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
            var repo = new VehicleRentalRepo();
            return await repo.GetAllRentals();
        }

        [HttpGet("GetAllVehicles")]
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            var repo = new VehicleRentalRepo();
            return await repo.GetAllVehicles();
        }

        [HttpPost("StartVehicleRent")]
        public async Task<IActionResult> StartVehicleRent(string registrationNumber, string personalIdNumber, int rentStartDistance_km)
        {
            var rows = await _vehicleRentalRepo.StartVehicleRent(registrationNumber.ToUpper(), personalIdNumber, rentStartDistance_km);
                
            if(rows > 0) 
                return Ok(rows);
            else
                return BadRequest($"Cannot rent the unknown car with registration number {registrationNumber.ToUpper()}");
        }

        [HttpPost("EndVehicleRent")]
        public async Task<IActionResult> EndVehicleRent(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            var rows = await _vehicleRentalRepo.EndVehicleRent(registrationNumber.ToUpper(), personalIdNumber, rentEndDistance_km);

            if (rows > 0)
                return Ok(rows);
            else
                return BadRequest($"Failed to end a rent for a car with registration number {registrationNumber.ToUpper()}. Is rentEndDistance_km correct?");
        }
    }
}
