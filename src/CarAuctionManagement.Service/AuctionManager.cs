namespace CarAuctionManagement.Service
{
    using System;
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Model.Exceptions;
    using CarAuctionManagement.Repository;

    public class AuctionManager : IAuctionManager
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IAuctionRepository auctionRepository;

        public AuctionManager(IVehicleRepository vehicleRepository, IAuctionRepository auctionRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.auctionRepository = auctionRepository;
        }

        public async Task BidAsync(int vehicleId, double bidAmount)
        {
            var auction = await GetActiveActionAsync(vehicleId);

            if (bidAmount <= auction.HigherBid)
            {
                throw new InvalidBidException(bidAmount, vehicleId);
            }

            if (bidAmount < auction.Vehicle.StartingBid)
            {
                throw new InvalidBidException(bidAmount, vehicleId);
            }

            auction.HigherBid = bidAmount;
            await auctionRepository.UpdateAsync(auction);

            return;
        }

        public async Task EndAuctionAsync(int vehicleId)
        {
            var auction = await GetActiveActionAsync(vehicleId);

            auction.EndingDate = DateTime.UtcNow;
            await auctionRepository.UpdateAsync(auction);

            return;
        }

        public async Task StartAuctionAsync(int vehicleId)
        {
            var existingAuction = await auctionRepository.GetActiveAuction(vehicleId);

            if (existingAuction != null)
            {
                throw new InvalidAuctionException($"There is already an active auction for a Vehicle with the identifier {vehicleId}");
            }

            var vehicle = await vehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new InvalidAuctionException($"There is no Vehicle with the identifier {vehicleId} in the inventory");
            }

            var newAuction = new Auction
            {
                Vehicle = vehicle,
                StartingDate = DateTime.UtcNow,
            };

            await auctionRepository.AddAsync(newAuction);
            return;
        }

        private async Task<Auction> GetActiveActionAsync(int vehicleId)
        {
            var auction = await auctionRepository.GetActiveAuction(vehicleId);

            if (auction == null)
            {
                throw new InvalidAuctionException($"There is no active auction for a Vehicle with the identifier {vehicleId}");
            }

            return auction;
        }
    }

}
