namespace VehicleRentalApi.TwoWheelers.Bike.Models
{
    public class BikeBooking
    {
        public int BookingNumber { get; set; }
        public int Id { get; set; }
        public string PersonalIdNumber { get; set; }
        public DateTime RentStartTime { get; set; }
        public DateTime RentEndTime { get; set; }
        public double Cost { get; set; }
     }
}
