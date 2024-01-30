namespace CarAuctionManagement.Model
{
    public class VehicleSUV : Vehicle
    {
        public override VehicleType Type => VehicleType.SUV;

        public double LoadCapacity { get; set; }
    }
}
