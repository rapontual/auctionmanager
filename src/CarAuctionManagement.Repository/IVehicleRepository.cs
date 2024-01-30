namespace CarAuctionManagement.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CarAuctionManagement.Model;

    public interface IVehicleRepository
    {
        Task AddAsync(Vehicle entity);

        Task<Vehicle?> GetByIdAsync(int id);

        Task<List<Vehicle>> SearchAsync(Func<Vehicle, bool> predicate);
    }
}
