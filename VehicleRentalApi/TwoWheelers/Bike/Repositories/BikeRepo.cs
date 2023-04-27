using System.Data.SqlClient;
using System.Data;
using VehicleRentalApi.TwoWheelers.Bike.Models;
using VehicleRentalApi.TwoWheelers.Bike.Repositories;
using Dapper;
using System.Reflection;

namespace VehicleRentalApi.TwoWheelers.ManualBike.Repositories
{ 
    public class BikeRepo : IBikeRepo
    {
        public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ManualBikeDb;Trusted_Connection=True;");

        public IEnumerable<BikeBooking> GetAllManualBikeRentals()
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = @"SELECT Id, PersonalIdNumber, RentStartTime, RentEndTime, Cost " +
                             "FROM Booking";
                var vehicleRentals = connection.Query<BikeBooking>(query);

                return vehicleRentals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw ex;
            }
        }

        public Task<int> StartRent(string bikeId, string PersonalIdNumber)
        {
            throw new NotImplementedException();
        }

        public Task<int> EndRent(string bikeId, string PersonalIdNumber)
        {
            throw new NotImplementedException();
        }
    }
}
