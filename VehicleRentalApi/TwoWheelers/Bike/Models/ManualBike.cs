using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public class ManualBike : IBike
    {
        double IBike.GetCost(BikeBooking booking)
        {
            return 200;
        }
    }
}
