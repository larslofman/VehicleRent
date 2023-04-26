namespace VehicleRentalApi.Models
{
    public class PassengerCar : Vehicle   //, ICalculator
    {
        public PassengerCar(Vehicle v)
        {
            RegistrationNumber = v.RegistrationNumber;
            Category = v.Category;
            Distance_km = v.Distance_km;
        }

        public override double GetCost(Category category, Booking booking)
        {
            TimeSpan diffResult = booking.RentEndTime - booking.RentStartTime;

            return category.TwentyFourHourBasePrice * Math.Max(diffResult.Days,1);
        }
    }
}
