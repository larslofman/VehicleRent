namespace VehicleRentalApi.Models
{
    public interface IBooking
    {
        float GetPrice { get; }
    }

    public class VehicleRental
    {
        public int BookingNumber { get; set; }
        //public Vehicle RentedVehicle { get; set; }
        public string RegistrationNumber {get; set;}
        public string PersonalIdNumber { get; set; }
        public DateTime RentStartTime { get; set; }
        public int RentStartDistance_km { get; set; }
	    public DateTime RentEndTime { get; set; }
        public int RentEndDistance_km { get; set; }

        //public Booking(int bookingNumber, Vehicle rentedVehicle, string personIdentityNumber, DateTime RentStartTime, int RentStartDistance_km, DateTime RentEndTime, int RentEndDistance_km, float price)
        //{
        //    BookingNumber = bookingNumber;
        //    RentedVehicle = rentedVehicle;
        //    PersonIdentityNumber = personIdentityNumber;
        //    RentStartTime = RentStartTime;
        //    RentStartDistance_km = RentStartDistance_km;
        //    RentEndTime = RentEndTime;
        //    RentEndDistance_km = RentEndDistance_km;
        //    Price = price;
        //}

        public float Price { get; set; }

        public void SaveStart()
        {

        }
        public void SaveStop()
        {

        }
    }
}
