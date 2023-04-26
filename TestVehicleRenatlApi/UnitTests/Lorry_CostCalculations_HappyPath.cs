using VehicleRentalApi.Models;
using VehicleRentalApi.Factories;

namespace VehicleRentalApiTest.UnitTests
{
    [TestClass]
    public class Lorry_CostCalculations_HappyPath
    {
        // Formula for cost for Lorry:
        // dayRent * days * 1,5 + kilometrePrice * kilometres *1,5

        [DataTestMethod]
        [DataRow("L")]
        public void CalulateCostForLorry(string categoryCode)
        {
            // Example: (320 * 3 * 1.3) + (10 * 100 * 1.5) = 2748

            // Arrange

            const int cDayRent = 320;                            // Should be the same as in table Category
            const int cKilometreBasePrice = 10;                  // Should be the same as in table Category

            Vehicle v = new Vehicle { CategoryCode = categoryCode };
            Exception expectedExcetpion = null;


            // Act

            var carCategoryToTest = VehicleFactory.CreateVehicleFromCategoryCode(v);

            var category = new Category { TwentyFourHourBasePrice = cDayRent,
                                          KilometreBasePrice = cKilometreBasePrice};
            var booking = new Booking
            {
                RentStartTime = new DateTime(2023, 4, 23),
                RentEndTime = new DateTime(2023, 4, 26),
                RentStartDistance_km = 0,
                RentEndDistance_km = 100,
            };

            var result = carCategoryToTest.GetCost(category, booking);

            //Assert

            Assert.AreEqual(result, 2748);
        }
    }
}