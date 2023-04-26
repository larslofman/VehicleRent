using VehicleRentalApi.Models;
using VehicleRentalApi.Factories;

namespace VehicleRentalApiTest.UnitTests
{
    [TestClass]
    public class PassengerCar_CostCalculations_WhenErrors
    {
        [TestMethod]
        public void GetExceptionWhenTryToCreateCarWithMissingCategoryCode()
        {
            // Arrange
            Vehicle v = new Vehicle { CategoryCode = "Z" };
            Exception expectedExcetpion = null;

            // Act
            try
            {
                var carCategoryToTest = VehicleFactory.CreateVehicleFromCategoryCode(v);
            }
            catch (Exception ex)
            {
                expectedExcetpion = ex;
            };

            //Assert
            Assert.AreEqual(expectedExcetpion.Message, "Vehicle category Z does not exist");
        }

        [DataTestMethod]
        [DataRow("C")]
        [DataRow("L")]
        [DataRow("P")]
        public void FailToCalulateCostWhenEndTimeIsMissing(string categoryCode)
        {
            // Arrange
            Vehicle v = new Vehicle { CategoryCode = categoryCode };
            Exception expectedExcetpion = null;

            // Act
            try
            {
                var carCategoryToTest = VehicleFactory.CreateVehicleFromCategoryCode(v);

                var category = new Category { TwentyFourHourBasePrice = 220 };
                var booking = new Booking
                {
                    RentStartTime = new DateTime(2023, 4, 22, 19, 42, 53),
                    //RentEndTime = new DateTime(2023, 4, 26, 8, 59, 29)
                };

                var result = carCategoryToTest.GetCost(category, booking);
            }
            catch (Exception ex)
            {
                expectedExcetpion = ex;
            };

            //Assert
            Assert.AreEqual(expectedExcetpion.Message, "Faulty value/values for Booking.RentEndTime and/or Booking.RentStartTime.");
        }
    }
}