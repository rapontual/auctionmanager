namespace CarAuctionManagement.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;
    using CarAuctionManagement.Model.Exceptions;

    public class VehicleRepository : IVehicleRepository
    {
        private static readonly List<Vehicle> vehicles = new List<Vehicle>();

        public VehicleRepository()
        {
            // to have a base data for test
            if (!vehicles.Any())
            {
                AddInitalVehicles();
            }
        }

        public Task AddAsync(Vehicle vehicle)
        {
            if (vehicles.Any(v => v.Id == vehicle.Id))
            {
                throw new DuplicateEntityException(nameof(Vehicle));
            }

            vehicles.Add(vehicle);

            return Task.CompletedTask;
        }

        public Task<Vehicle?> GetByIdAsync(int id)
        {
            return Task.FromResult(vehicles.FirstOrDefault(v => v.Id == id));
        }

        public Task<List<Vehicle>> SearchAsync(Func<Vehicle, bool> predicate)
        {
            return Task.FromResult(vehicles.Where(predicate).ToList());
        }

        private void AddInitalVehicles()
        {
            vehicles.Add(new Vehicle
            {
                Id = 100,
                Manufacturer = "BMW",
                Model = "X3",
                Type = VehicleType.SUV,
                Year = 2022,
                StartingBid = 10000
            });
            vehicles.Add(new Vehicle
            {
                Id = 101,
                Manufacturer = "BMW",
                Model = "X5",
                Type = VehicleType.SUV,
                Year = 2021,
                StartingBid = 13000
            });
            vehicles.Add(new Vehicle
            {
                Id = 102,
                Manufacturer = "Audi",
                Model = "A4",
                Type = VehicleType.Sedan,
                Year = 2022,
                StartingBid = 6000
            });
            vehicles.Add(new Vehicle
            {
                Id = 103,
                Manufacturer = "Mazda",
                Model = "MX3",
                Type = VehicleType.Hatchback,
                Year = 2021,
                StartingBid = 4000
            });
            vehicles.Add(new Vehicle
            {
                Id = 104,
                Manufacturer = "Mercedes-Benz",
                Model = "Actros",
                Type = VehicleType.Truck,
                Year = 2021,
                StartingBid = 25000
            });
        }
    }
}
