namespace CarAuctionManagement.Service.Tests
{
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Repository;
    using Moq;

    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> vehicleRepositoryMock;
        private readonly VehicleService vehicleService;

        public VehicleServiceTests()
        {
            vehicleRepositoryMock = new Mock<IVehicleRepository>();
            vehicleService = new VehicleService(vehicleRepositoryMock.Object);
        }

        [Fact]
        public async Task AddVehicleAsync_ShouldAndCallRepositoryAddAsync()
        {
            var vehicle = new Vehicle
            {
                Id = 123
            };

            await vehicleService.AddVehicleAsync(vehicle);

            vehicleRepositoryMock
                .Verify(v => v.AddAsync(It.Is<Vehicle>(o => o.Id == vehicle.Id)));
        }

        [Fact]
        public async Task SearchAsyc_ShouldSearchWithVehicleTypeAndCallRepositoryAsync()
        {
            var result = await vehicleService.SearchAsyc(VehicleType.Truck, null, null, null);

            vehicleRepositoryMock
                .Verify(v => v.SearchAsync(It.IsAny<Func<Vehicle, bool>>()));
        }
    }
}