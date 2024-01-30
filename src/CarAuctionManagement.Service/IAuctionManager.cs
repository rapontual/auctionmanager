namespace CarAuctionManagement.Service
{
    using System.Threading.Tasks;

    public interface IAuctionManager
    {
        Task StartAuctionAsync(int vehicleId);

        Task EndAuctionAsync(int vehicleId);

        Task BidAsync(int vehicleId, double bidAmount);
    }
}
