using VehicleRentalApi.Models;
using VehicleRentalApi.Factories;

namespace VehicleRentalApiTest.UnitTests.Vehicles
{
    [TestClass]
    public class Combi_CostCalculations_HappyPath
    {
        // Formula for cost for Combi:
        // dayRent * days * 1,3 + kilometrePrice * kilometres

        [DataTestMethod]
        [DataRow("C")]
        public void CalulateCostForCombi(string categoryCode)
        {
            // Example: (270 * 3 * 1,3) + (5 * 100) = 1553

            // Arrange

            const int cDayRent = 270;                            
            const int cKilometreBasePrice = 5;                   

            Vehicle v = new Vehicle { CategoryCode = categoryCode };

            // Act

            var carCategoryToTest = VehicleFactory.CreateVehicleFromCategoryCode(v);

            var category = new Category
            {
                TwentyFourHourBasePrice = cDayRent,
                KilometreBasePrice = cKilometreBasePrice
            };
            var booking = new Booking
            {
                RentStartTime = new DateTime(2023, 4, 23),
                RentEndTime = new DateTime(2023, 4, 26),
                RentStartDistance_km = 0,
                RentEndDistance_km = 100,
            };

            var result = carCategoryToTest.GetCost(category, booking);

            //Assert

            Assert.AreEqual(result, 1553);
        }
    }
}