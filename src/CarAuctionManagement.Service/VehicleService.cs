namespace CarAuctionManagement.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Repository;

    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(IVehicleRepository repository)
        {
            this.vehicleRepository = repository;
        }

        public Task AddVehicleAsync(Vehicle vehicle)
        {
            return vehicleRepository.AddAsync(vehicle);
        }

        public Task<List<Vehicle>> SearchAsyc(VehicleType? vehicleType, string? manufacturer, string? model, int? year)
        {
            // here i'm not considering case or culture
            return vehicleRepository.SearchAsync(v => 
                (!vehicleType.HasValue || v.Type == vehicleType.Value)
                && (!year.HasValue || v.Year == year)
                && (string.IsNullOrWhiteSpace(manufacturer) || v.Manufacturer == manufacturer)
                && (string.IsNullOrWhiteSpace(model) || v.Model == model));
        }
    }
}
