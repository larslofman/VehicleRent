using Microsoft.AspNetCore.Mvc;
using VehicleRentalApi.Repositories;
using VehicleRentalApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IEnumerable<VehicleRental> GetAllVehicleRentals()
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



        //[HttpPost(Name = "EndVehicleRent")]
        //public void EndVehicleRent(VehicleRental vehicle)
        //{
        //    vehicle.SaveStop();
        //}


        // GET: api/<ValuesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<ValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<ValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<ValuesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ValuesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
