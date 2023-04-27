using VehicleRentalApi.Factories;
using VehicleRentalApi.Models;

namespace VehicleRentalApiTest.UnitTests.Vehicles
{
    [TestClass]
    public class PassengerCar_CostCalculations_HappyPath
    {
        [DataTestMethod]
        [DataRow("P")]
        public void CalulateCostForPassengerCar(string categoryCode)
        {
            // Example: 250 * 3 = 750

            // Arrange

            const int cDayRent = 250;                           
            const int cKilometreBasePrice = 0;                  

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

            Assert.AreEqual(result, 750);
        }

        [DataTestMethod]
        [DataRow("P")]
        public void FourDayRentOfPassengerCarCostIsCorrect(string categoryCode)
        {
            // Arrange

            const int Const_TwentyFourHourBasePrice = 250;
            var volvo = new PassengerCar(new Vehicle
            {
                CategoryCode = categoryCode
            });
            var category = new Category { Code = categoryCode, TwentyFourHourBasePrice = Const_TwentyFourHourBasePrice };
            var booking = new Booking
            {
                RentStartTime = new DateTime(2023, 4, 22),
                RentEndTime = new DateTime(2023, 4, 26)
            };

            // Act

            var result = volvo.GetCost(category, booking);

            //Assert

            Assert.AreEqual(4 * Const_TwentyFourHourBasePrice, result);
        }

        [TestMethod]
        public void ThreeDayRentOfPassengerCarCostIsCorrect()
        {
            // Arrange

            const int Const_TwentyFourHourBasePrice = 250; 
            var volvo = new PassengerCar(new Vehicle
            {
                CategoryCode = "P"
            });
            var category = new Category { Code = "P", TwentyFourHourBasePrice = Const_TwentyFourHourBasePrice };

            var booking = new Booking
            {
                RentStartTime = new DateTime(2023, 4, 22, 19, 42, 53),
                RentEndTime = new DateTime(2023, 4, 26, 8, 59, 29)
            };

            // Act

            var result = volvo.GetCost(category, booking);

            //Assert

            Assert.AreEqual(3 * Const_TwentyFourHourBasePrice, result);
        }

        [TestMethod]
        public void ZeroDayRentOfPassengerCarCostIsEqualToOneDayRent()
        {
            // Arrange

            const int Const_TwentyFourHourBasePrice = 250; 
            var volvo = new PassengerCar(new Vehicle
            {
                CategoryCode = "P"
            });
            var category = new Category { Code = "P", TwentyFourHourBasePrice = Const_TwentyFourHourBasePrice };

            var booking = new Booking
            {
                RentStartTime = new DateTime(2023, 4, 26),
                RentEndTime = new DateTime(2023, 4, 26)
            };

            // Act

            var result = volvo.GetCost(category, booking);

            //Assert

            Assert.AreEqual(1 * Const_TwentyFourHourBasePrice, result);
        }
    }
}