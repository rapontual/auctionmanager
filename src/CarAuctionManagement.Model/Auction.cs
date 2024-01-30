namespace CarAuctionManagement.Model
{
    public class Auction
    {
        public required Vehicle Vehicle { get; set; }

        public double HigherBid { get; set; }

        public DateTime StartingDate { get; set; }

        public DateTime? EndingDate { get; set; }
    }
}
