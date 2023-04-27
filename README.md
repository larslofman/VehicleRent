# VehicleRent
Lab project to rent cars

1. Switch to the branch master
2. Copy the SQL-code in Db-script to:
   a: Create the databse VehicleRentDb
   b: Create and poulate the 3 tables:
      Booking
      Category
      Vehicle
      
3. Change the hardcode database connection string to connect to the SQL Server instance you will use.

      VehicleRentalRepo.cs:
      
      public static IDbConnection DbConnection => new SqlConnection("Server=localhost\\SQLEXPRESS;Database=VehicleRentDb;Trusted_Connection=True;");
      
4. Start the program

Swagger should start with 4 routes to call:

/VehicleRental/GetAllVehiclesRentals
/VehicleRental/GetAllVehicles
/VehicleRental/StartVehicleRent
/VehicleRental/EndVehicleRent

Some vehicles rents (bookings) are already populated in the database.

- You can only rent a vehicle that is in the database. Check those with GetAllVehicles
- You can only start one rent at a time for one vehicle

The changes are all made in the table Booking.

(The folder TwoWheelers are only for one test sofar.)




