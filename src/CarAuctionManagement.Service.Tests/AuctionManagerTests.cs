namespace CarAuctionManagement.Service.Tests
{
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Model.Exceptions;
    using CarAuctionManagement.Repository;
    using Moq;

    public class AuctionManagerTests
    {
        private readonly Mock<IVehicleRepository> vehicleRepositoryMock;
        private readonly Mock<IAuctionRepository> auctionRepositoryMock;
        private AuctionManager auctionManager;

        public AuctionManagerTests()
        {
            vehicleRepositoryMock = new Mock<IVehicleRepository>();
            auctionRepositoryMock = new Mock<IAuctionRepository>();
            auctionManager = new AuctionManager(vehicleRepositoryMock.Object, auctionRepositoryMock.Object);
        }

        [Fact]
        public async Task BidAsync_ShouldBidWhenValidValuesAndActiveAuctionAsync()
        {
            // Arrange
            double bid = 20;

            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = 10,
                    StartingBid = 10,
                }
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(auction);

            // Act & Assert
            await auctionManager.BidAsync(auction.Vehicle.Id, bid);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.Is<Auction>(o => o.HigherBid == bid)));

        }

        [Fact]
        public async Task BidAsync_ShouldThrowExceptionWhenAuctionIsntExistAsync()
        {
            // Arrange
            var id = 20;
            var expectedExceptionMessage = $"There is no active auction for a Vehicle with the identifier {id}";

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(null as Auction);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidAuctionException>(() => auctionManager.BidAsync(id, 20));

            Assert.Equal(result.Message, expectedExceptionMessage);
            
            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task BidAsync_ShouldThrowExceptionWhenBidIsLowerThanHigherBidAsync()
        {
            // Arrange
            double bid = 11;

            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = 10,
                    StartingBid = 10,
                },
                HigherBid = 12
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(auction);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidBidException>(() => auctionManager.BidAsync(auction.Vehicle.Id, bid));

            Assert.NotNull(result);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task BidAsync_ShouldThrowExceptionWhenBidIsLowerThanStartingBidAsync()
        {
            // Arrange
            double bid = 10;

            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = 10,
                    StartingBid = 0,
                },
                HigherBid = 12
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(auction);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidBidException>(() => auctionManager.BidAsync(auction.Vehicle.Id, bid));

            Assert.NotNull(result);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task EndAuctionAsync_ShouldEndAuctionAsync()
        {
            // Arrange
            var expectedDate = DateTime.UtcNow;
            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = 10,
                    StartingBid = 0,
                },
                HigherBid = 12
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(auction);

            // Act & Assert
            await auctionManager.EndAuctionAsync(auction.Vehicle.Id);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.Is<Auction>(a => a.EndingDate >= expectedDate)));
        }

        [Fact]
        public async Task EndAuctionAsync_ShouldThrowExceptionWhenAuctionIsntExistAsync()
        {
            // Arrange
            var id = 20;
            var expectedExceptionMessage = $"There is no active auction for a Vehicle with the identifier {id}";

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(null as Auction);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidAuctionException>(() => auctionManager.EndAuctionAsync(id));

            Assert.Equal(result.Message, expectedExceptionMessage);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task StartAuctionAsync_ShouldThrowExceptionWhenAuctionAlreadyExistsAsync()
        {
            // Arrange
            var id = 20;
            var expectedExceptionMessage = $"There is already an active auction for a Vehicle with the identifier {id}";
            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = id,
                    StartingBid = 0,
                },
                HigherBid = 12
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(auction);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidAuctionException>(() => auctionManager.StartAuctionAsync(id));

            Assert.Equal(result.Message, expectedExceptionMessage);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task StartAuctionAsync_ShouldThrowExceptionWhenVehicleDoesntExistAsync()
        {
            // Arrange
            var id = 20;
            var expectedExceptionMessage = $"There is no Vehicle with the identifier {id} in the inventory";
            var auction = new Auction
            {
                Vehicle = new Vehicle
                {
                    Id = id,
                    StartingBid = 0,
                },
                HigherBid = 12
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(null as Auction);

            vehicleRepositoryMock
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(null as Vehicle);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidAuctionException>(() => auctionManager.StartAuctionAsync(id));

            Assert.Equal(result.Message, expectedExceptionMessage);

            auctionRepositoryMock
                .Verify(v => v.UpdateAsync(It.IsAny<Auction>()), Times.Never);
        }

        [Fact]
        public async Task StartAuctionAsync_ShouldStartAsync()
        {
            // Arrange
            var id = 20;
            var expectedExceptionMessage = $"There is no Vehicle with the identifier {id} in the inventory";
            var vehicle = new Vehicle
            {
                Id = id,
                StartingBid = 0,
            };

            auctionRepositoryMock
                .Setup(s => s.GetActiveAuction(It.IsAny<int>()))
                .ReturnsAsync(null as Auction);

            vehicleRepositoryMock
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(vehicle);

            // Act & Assert
            await auctionManager.StartAuctionAsync(id);

            auctionRepositoryMock
                .Verify(v => v.AddAsync(It.IsAny<Auction>()));
        }
    }
}
