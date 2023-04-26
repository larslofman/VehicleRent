using VehicleRentalApi.Models;

namespace VehicleRentalApi.Factories
{
    public static class VehicleFactory
    {
        public static Vehicle CreateVehicleFromCategoryCode(Vehicle vehicle)
        {
             if (vehicle.CategoryCode.Equals("C"))
                return new Combi(vehicle);
            if (vehicle.CategoryCode.Equals("L"))
                return new Lorry(vehicle);
            if (vehicle.CategoryCode.Equals("P"))
                return new PassengerCar(vehicle);

            throw new ApplicationException($"Vehicle category {vehicle.CategoryCode} does not exist");
        }
    }
}
