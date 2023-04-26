﻿namespace VehicleRentalApi.Models
{
    public interface IVehicleRental
    {
        float GetCost();
    }
    public class Lorry : Vehicle  //, ICalculator
    {
        public Lorry(Vehicle v)
        {
            RegistrationNumber = v.RegistrationNumber;
            Category = v.Category;
            Distance_km = v.Distance_km;
            CargoSpace_m2 = v.CargoSpace_m2;
        }
        public override double GetCost(Category category, Booking booking)
        {
            TimeSpan days = booking.RentEndTime - booking.RentStartTime;

            return category.TwentyFourHourBasePrice * Math.Max(days.Days, 1) * 1.3 +
                   category.KilometreBasePrice * (booking.RentEndDistance_km - booking.RentStartDistance_km) * 1.5;
        }
     }
}
