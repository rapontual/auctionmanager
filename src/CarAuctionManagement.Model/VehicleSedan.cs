namespace CarAuctionManagement.Model
{
    public class VehicleSedan : Vehicle
    {
        public override VehicleType Type => VehicleType.Sedan;

        public byte NumberOfDoors { get; set; }
    }
}
