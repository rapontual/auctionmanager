namespace CarAuctionManagement.Model
{
    public class VehicleHatchback : Vehicle
    {
        public override VehicleType Type => VehicleType.Hatchback;

        public byte NumberOfDoors { get; set; }
    }
}
