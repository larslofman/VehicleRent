using VehicleRentalApi.TwoWheelers.Bike.Models;

namespace VehicleRentalApi.TwoWheelers.Bike.Repositories
{
    public interface IBikeRepo
    {
        public IEnumerable<BikeBooking> GetAllManualBikeRentals();
        public Task<int> StartRent(string bikeId, string PersonalIdNumber);
        public Task<int> EndRent(string bikeId, string PersonalIdNumber);
    }
}