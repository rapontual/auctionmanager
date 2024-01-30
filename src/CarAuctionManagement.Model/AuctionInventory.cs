namespace CarAuctionManagement.Model
{
    using System.Collections.Generic;

    public class AuctionInventory
    {
        public IList<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
