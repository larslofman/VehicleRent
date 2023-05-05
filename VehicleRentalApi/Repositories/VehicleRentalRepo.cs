using System.Data;
using System.Data.SqlClient;
using Dapper;
using VehicleRentalApi.Models;
using VehicleRentalApi.Factories;
using System.Reflection;

namespace VehicleRentalApi.Repositories
{
    public class VehicleRentalRepo : IVehicleRentalRepo
    {

        public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=VehicleRentDb;Trusted_Connection=True;");

        public async Task<IEnumerable<Booking>> GetAllRentals()
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = @"SELECT BookingNumber, RegistrationNumber, PersonalIdNumber, RentStartTime, RentStartDistance_km, " +
                             "RentEndTime, RentEndDistance_km, Cost " +
                             "FROM Booking";
                var vehicleRentals = await connection.QueryAsync<Booking>(query);

                return vehicleRentals;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw ex;
            }
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = @"SELECT RegistrationNumber, Category " +
                             "FROM Vehicle";
                var vehicles = await connection.QueryAsync<Vehicle>(query);

                return vehicles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw ex;
            }
        }

        async Task<int> IVehicleRentalRepo.StartVehicleRent(string registrationNumber, string personalIdNumber, int rentstartDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var vehicle = await GetVehicle(registrationNumber);
                if (vehicle == null)
                    throw new ApplicationException($"Cannot book an unknown car: {registrationNumber}");

                string insertQuery = @"INSERT INTO Booking (RegistrationNumber, PersonalIdNumber, RentStartTime, RentstartDistance_km)
                                       VALUES(@registrationNumber, @personalIdNumber, @rentStartTime, @rentstartDistance_km)";                                     ;

                var result = await connection.ExecuteAsync(insertQuery, new
                {
                    registrationNumber,
                    personalIdNumber,
                    rentStartTime = DateTime.Now,
                    rentstartDistance_km
                });

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                return 0;
            }
        }

        async Task<int> IVehicleRentalRepo.EndVehicleRent(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var cost = await CalculateCost(registrationNumber, personalIdNumber, rentEndDistance_km);

                string updateQuery = @"UPDATE Booking 
                                       SET RentEndTime = getdate(), 
                                       RentEndDistance_km = @rentEndDistance_km, 
                                       Cost = @cost
                                       WHERE RegistrationNumber = @registrationNumber 
                                       AND PersonalIdNumber = @personalIdNumber 
                                       AND RentEndTime IS NULL 
                                       AND ISNULL(@rentEndDistance_km, 0) > ISNULL(RentStartDistance_km, 0)";

                var result = await connection.ExecuteAsync(updateQuery, new
                {
                    rentEndDistance_km,
                    cost,
                    registrationNumber,
                    personalIdNumber,
                });

                if (result == 0)
                {
                    throw new ApplicationException("There distance meter has decreased");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                return 0;
            }
        }

        async Task <double> CalculateCost(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            var vehicle = await GetVehicle(registrationNumber);
            var category = await GetCategory(vehicle.CategoryCode);
            var booking = await GetBooking(registrationNumber, personalIdNumber, rentEndDistance_km);

            return vehicle.GetCost(category, booking);
        }

        async Task<Booking> GetBooking(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = $"SELECT BookingNumber, RegistrationNumber, PersonalIdNumber, RentStartTime, RentStartDistance_km, RentEndTime, RentEndDistance_km, Cost " +
                            $"FROM Booking " +
                            $"WHERE RegistrationNumber = '{registrationNumber}' " +
                            $"AND PersonalIdNumber = '{personalIdNumber}' " +
                            $"AND RentEndTime IS NULL";

                var booking = connection.Query<Booking>(query).SingleOrDefault();
                if (booking != null)
                {
                    booking.RentEndDistance_km = rentEndDistance_km;
                    booking.RentEndTime = DateTime.Now;
                }

                return booking;
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw;
            }
        }

        async Task<Category> GetCategory(string code)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = $"SELECT Code, Description, TwentyFourHourBasePrice, KilometreBasePrice " +
                            $"FROM Category " +
                            $"WHERE Code = '{code}'";

                var category = connection.Query<Category>(query).SingleOrDefault();
                return category;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw;
            }
        }

        async Task<Vehicle> GetVehicle(string registrationNumber)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = $"SELECT RegistrationNumber, Category as CategoryCode " +
                            $"FROM Vehicle " +
                            $"WHERE RegistrationNumber = '{registrationNumber}'";

                var vehicle = connection.Query<Vehicle>(query).SingleOrDefault();
                if (vehicle == null)
                    throw new ApplicationException("Vehicle does not exist");

                Vehicle v = VehicleFactory.CreateVehicleFromCategoryCode(vehicle);

                return v;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application exception in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
                throw;
            }
        }
    }
}
