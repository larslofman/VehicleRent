using VehicleRentalApi.TwoWheelers.Bike.Models;

namespace VehicleRentalApiTest.UnitTests.Bikes
{
    [TestClass]
    public class BikeTest
    {
        [TestMethod]
        public void CalulateCostWithInterface()
        {
            // Test to calculate with the interface method GetCost (Costs are hardcoded in the classes ManualBike and MotorBike. They should be calculated
            // with the data from an instance of the class BikeBooking that is an inparameter to the GetHost interface-method).

            List<IBike> list = new List<IBike>();

            list.Add(new ManualBike());
            list.Add(new MotorBike());

            double totalCost = 0;
            foreach (var bike in list)
            {
                totalCost += bike.GetCost(new BikeBooking());
            }
            Assert.AreEqual(totalCost, 1200);
        }
    }
}