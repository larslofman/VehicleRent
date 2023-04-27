namespace VehicleRentalApi.Models
{
    public class Combi : Vehicle
    {
        public Combi(Vehicle v)
        {
            RegistrationNumber = v.RegistrationNumber;
            CategoryCode = v.CategoryCode;
            Distance_km = v.Distance_km;
        }
        public override double GetCost(Category category, Booking booking)
        {
            TimeSpan diffResult = booking.RentEndTime - booking.RentStartTime;
            if (diffResult.Days < 0)
                throw new ApplicationException("Faulty value/values for Booking.RentEndTime and/or Booking.RentStartTime.");

            return category.TwentyFourHourBasePrice * Math.Max(diffResult.Days, 1) * 1.3 + 
                   category.KilometreBasePrice * (booking.RentEndDistance_km - booking.RentStartDistance_km);
        }
    }
}