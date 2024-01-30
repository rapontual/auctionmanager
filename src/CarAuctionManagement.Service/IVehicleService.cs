namespace CarAuctionManagement.Service
{
    using CarAuctionManagement.Model;

    public interface IVehicleService
    {
        Task AddVehicleAsync(Vehicle vehicle);

        Task<List<Vehicle>> SearchAsyc(
            VehicleType? vehicleType,
            string? manufacturer,
            string? model,
            int? year);
    }
}
