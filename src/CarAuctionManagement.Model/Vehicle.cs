namespace CarAuctionManagement.Model
{
    public class Vehicle
    {
        public int Id { get; set; }

        // this property as initially only get since the class tha extends Vehicle sets
        // was added the get only because I'm using the same model for the api request
        public virtual VehicleType Type { get; set; }

        // this could be another type, I left as string to simplify this test
        public string Manufacturer { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public double StartingBid { get; set; }
    }
}
