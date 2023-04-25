using System.Data;
using System.Data.SqlClient;
using Dapper;
using VehicleRentalApi.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace VehicleRentalApi.Repositories
{
    public class VehicleRentalRepo : IVehicleRentalRepo
    {

        public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=VehicleRentDb;Trusted_Connection=True;Connection timeout=30;");

        //public async Task<IEnumerable<VehicleRental>> GetAllRentals()
        public IEnumerable<VehicleRental> GetAllRentals()
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();
                var query = "SELECT BookingNumber, RegistrationNumber, PersonalIdNumber, RentStartTime, RentStartDistance_km, RentEndTime, RentEndDistance_km, Price FROM Booking";
                List<VehicleRental> vehicleRentals = connection.Query<VehicleRental>(query).ToList();

                return vehicleRentals;
            }
            catch (Exception ex)
            {
                //_logger.LogWarning($"{DateTime.Now}: Exception i LicensRepo/GetLicenses: {ex.Message}");
                return null;
            }
        }

        void IVehicleRentalRepo.StartVehicleRent(string RegistrationNumber, string PersonalIdNumber, int RentstartDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

               string insertQuery = @"INSERT INTO Booking (RegistrationNumber, PersonalIdNumber, RentStartTime, RentstartDistance_km) " +
                                     "VALUES(@RegistrationNumber, @PersonalIdNumber, @RentStartTime, @RentstartDistance_km)";

                var result = connection.Execute(insertQuery, new
                {
                    RegistrationNumber,
                    PersonalIdNumber,
                    RentStartTime = DateTime.Now,
                    RentstartDistance_km
                });
                return;
            }
            catch (Exception ex)
            {
                //_logger.LogWarning($"{DateTime.Now}: \nException: {ex.Message}");1

                //return -1;
                return;
            }
        }

        void IVehicleRentalRepo.EndVehicleRent(string RegistrationNumber, string PersonalIdNumber, int RentEndDistance_km)
        {
            try
            {
                using var connection = DbConnection;
                connection.Open();

                string insertQuery = @$"UPDATE Booking " +
                                      "SET RentEndTime = @RentEndTime, RentEndDistance_km = @RentEndDistance_km " +
                                      "WHERE RegistrationNumber = @RegistrationNumber " +
                                      "AND @PersonalIdNumber =  @PersonalIdNumber " +
                                      "AND RentEndTime IS NULL"; 

                var result = connection.Execute(insertQuery, new
                {
                    RegistrationNumber,
                    PersonalIdNumber,
                    RentEndTime = DateTime.Now,
                    RentEndDistance_km
                });
            }
            catch (Exception ex)
            {
                //_logger.LogWarning($"{DateTime.Now}: \nException: {ex.Message}");
                //return -1;
                return;
            }
        }
    }
}
