using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehicleRepository
    {
        Vehicle Create(Vehicle vehicle);
        Vehicle Update(Vehicle vehicle);
        void Delete(int id);
        VehicleViewModel GetById(int id);
        IEnumerable<VehicleViewModel> GetNewVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100);
        IEnumerable<VehicleViewModel> GetUsedVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100);
        IEnumerable<VehicleViewModel> GetAllVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100);
        VehicleInventoryViewModel GetVehicleInventoryReport();
        void AddFeatured(int vehicleId);
        void RemoveFeatured(int vehicleId);
        FeaturedVehiclesViewModel GetAllFeatures();
        void ChangeToSold(int id);
    }
}
