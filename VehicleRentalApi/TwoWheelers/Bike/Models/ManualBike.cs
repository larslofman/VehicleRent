using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public class ManualBike : IBike
    {
        public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=ManualBikeDb;Trusted_Connection=True;");

        double IBike.GetCost(BikeBooking booking)
        {
            return 200;
        }
    }
}
