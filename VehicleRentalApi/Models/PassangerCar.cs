namespace VehicleRentalApi.Models
{
    public class PassengerCar : Vehicle
    {
        public PassengerCar(Vehicle v)
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

            return category.TwentyFourHourBasePrice * Math.Max(diffResult.Days,1);
        }
    }
}