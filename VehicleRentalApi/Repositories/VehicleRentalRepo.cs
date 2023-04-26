using System.Data;
using System.Data.SqlClient;
using Dapper;
using VehicleRentalApi.Models;
using VehicleRentalApi.Factories;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace VehicleRentalApi.Repositories
{
    public class VehicleRentalRepo : IVehicleRentalRepo
    {

        public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=VehicleRentDb;Trusted_Connection=True;");

        public IEnumerable<Booking> GetAllRentals()
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var query = @"SELECT BookingNumber, RegistrationNumber, PersonalIdNumber, RentStartTime, RentStartDistance_km, " +
                             "RentEndTime, RentEndDistance_km, Cost " +
                             "FROM Booking";
                List<Booking> vehicleRentals = connection.Query<Booking>(query).ToList();

                return vehicleRentals;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void IVehicleRentalRepo.StartVehicleRent(string registrationNumber, string personalIdNumber, int rentstartDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                string insertQuery = @"INSERT INTO Booking (RegistrationNumber, PersonalIdNumber, RentStartTime, RentstartDistance_km) " +
                                      "VALUES(@registrationNumber, @personalIdNumber, @rentStartTime, @rentstartDistance_km)";

                var result = connection.Execute(insertQuery, new
                {
                    registrationNumber,
                    personalIdNumber,
                    rentStartTime = DateTime.Now,
                    rentstartDistance_km
                });
                return;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void IVehicleRentalRepo.EndVehicleRent(string RegistrationNumber, string PersonalIdNumber, int RentEndDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                var cost = CalculateCost(RegistrationNumber, PersonalIdNumber, RentEndDistance_km);

                string updateQuery = $"UPDATE Booking " +
                                     $"SET RentEndTime = getdate(), RentEndDistance_km = {RentEndDistance_km}, Cost = {cost} " +
                                     $"WHERE RegistrationNumber = '{RegistrationNumber}' " +
                                     $"AND PersonalIdNumber = '{PersonalIdNumber}' " +
                                     $"AND RentEndTime IS NULL";

                 var result = connection.Execute(updateQuery);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private double CalculateCost(string registrationNumber, string personalIdNumber, int rentEndDistance_km)
        {
            var vehicle = GetVehicle(registrationNumber, personalIdNumber);
            var category = GetCategory(vehicle.CategoryCode);
            var booking = GetBooking(registrationNumber, personalIdNumber);
            booking.RentEndDistance_km = rentEndDistance_km;
            booking.RentEndTime = DateTime.Now;

            return vehicle.GetCost(category, booking);
        }

        private Booking GetBooking(string registrationNumber, string personalIdNumber)
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
                return booking;
            } 
            catch (Exception ex)
            {
                throw;
            }
        }

        private Category GetCategory(string code)
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
            catch (Exception)
            {
                throw;
            }
        }

        private Vehicle GetVehicle(string registrationNumber, string personalIdNumber)
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
                Console.WriteLine($"Application exception: {ex.Message}");
                throw ex;
            }
        }
    }
}
