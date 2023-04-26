namespace VehicleRentalApi.Models
{
    public class Category
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double TwentyFourHourBasePrice { get; set; }
        public double KilometreBasePrice { get; set; }
    }
}
