namespace CarAuctionManagement.Repository
{
    using CarAuctionManagement.Model;

    public interface IAuctionRepository
    {
        Task AddAsync(Auction auction);

        Task UpdateAsync(Auction auction);

        Task<Auction?> GetActiveAuction(int id);

    }
}
