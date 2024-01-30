namespace CarAuctionManagement.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Model.Exceptions;

    public class AuctionRepository : IAuctionRepository
    {
        private static readonly IList<Auction> auctions = new List<Auction>();

        public Task AddAsync(Auction entity)
        {
            if (auctions.Any(v => v.Vehicle.Id == entity.Vehicle.Id && v.EndingDate is null))
            {
                throw new DuplicateEntityException(nameof(Auction));
            }

            auctions.Add(entity);

            return Task.CompletedTask;
        }

        public Task<Auction?> GetActiveAuction(int vehicleId)
        {
            return Task.FromResult(auctions.FirstOrDefault(v => v.Vehicle.Id == vehicleId && v.EndingDate is null));
        }

        public Task UpdateAsync(Auction auction)
        {
            var existingAuction = auctions.FirstOrDefault(a => a.Vehicle.Id == auction.Vehicle.Id);

            if (existingAuction != null)
            {
                existingAuction.HigherBid = auction.HigherBid;
                existingAuction.EndingDate = auction.EndingDate;
            }

            return Task.CompletedTask;
        }
    }
}
