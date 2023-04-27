namespace VehicleRentalApi.Models
{
     public class Booking
    {
        public int BookingNumber { get; set; }
        public string RegistrationNumber {get; set;}
        public string PersonalIdNumber { get; set; }
        public DateTime RentStartTime { get; set; }
        public int RentStartDistance_km { get; set; }
	    public DateTime RentEndTime { get; set; }
        public int RentEndDistance_km { get; set; }
    }
}
