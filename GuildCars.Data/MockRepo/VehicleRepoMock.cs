using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Queries.Reports;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.MockRepo
{
    public class VehicleRepoMock : IVehicleRepository
    {
        private static List<Vehicle> _vehicles = new List<Vehicle>()
        {
            new Vehicle(){ Color="black",Description="its a car",Interior="black",MakeId=1,ModelId=1,Mileage=100,VehicleId=1,
                             MSRP =15000m,SalePrice=13000M,Sold=false,Style="Car",Transmission="Auto",Type="New",Vin="12390asjdb11da",Year=2016},
            new Vehicle(){ Color="Blue",Description="its a var",Interior="black",MakeId=2,ModelId=2,Mileage=130,VehicleId=2,
                             MSRP =16000m,SalePrice=14000M,Sold=false,Style="Van",Transmission="Auto",Type="New",Vin="91060asd1da",Year=2017}
        };

        private static List<FeaturedVehicle> _features = new List<FeaturedVehicle>()
        {
            new FeaturedVehicle(){FeaturedId = 1, VehicleId = 1},
            new FeaturedVehicle(){FeaturedId = 2, VehicleId = 2}
        };

        public void AddFeatured(int vehicleId)
        {
            var feature = new FeaturedVehicle();
            feature.FeaturedId = GetNextFeatureId();
            feature.VehicleId = vehicleId;
            _features.Add(feature);
        }

        public Vehicle Create(Vehicle vehicle)
        {
            vehicle.VehicleId = GetNextId();
            _vehicles.Add(vehicle);
            return vehicle;
        }

        public void Delete(int id)
        {
            _vehicles.RemoveAll(x => x.VehicleId == id);
        }

        public FeaturedVehiclesViewModel GetAllFeatures()
        {
            var returnFeatures = new FeaturedVehiclesViewModel();
            foreach (var f in _features)
            {
                var currentCar = GetById(f.VehicleId);
                var current = new FeatureViewModel()
                {
                    SalePrice = currentCar.SalePrice,
                    VehicleId = currentCar.VehicleId,
                    YearMakeModel = currentCar.Year.ToString() + " " + currentCar.Make + " " + currentCar.Model
                };
                returnFeatures.features.Add(current);
            }
            return returnFeatures;
        }

        public VehicleViewModel GetById(int id)
        {
            var foundVehicle = _vehicles.FirstOrDefault(x => x.VehicleId == id);

            MakeRepoMock makeRepo = new MakeRepoMock();
            var makes = makeRepo.GetAll();

            ModelRepoMock modelRepo = new ModelRepoMock();
            var models = modelRepo.GetAll();

            VehicleViewModel returnVehicle = new VehicleViewModel()
            {
                Color = foundVehicle.Color,
                Description = foundVehicle.Description,
                Interior = foundVehicle.Interior,
                Make = makes.FirstOrDefault(x => x.MakeId == foundVehicle.MakeId).Name,
                Model = models.FirstOrDefault(x => x.ModelId == foundVehicle.ModelId).MakeName,
                SalePrice = foundVehicle.SalePrice,
                Mileage = foundVehicle.Mileage,
                MSRP = foundVehicle.MSRP,
                Sold = foundVehicle.Sold,
                Style = foundVehicle.Style,
                Transmission = foundVehicle.Transmission,
                Type = foundVehicle.Type,
                VehicleId = foundVehicle.VehicleId,
                Vin = foundVehicle.Vin,
                Year = foundVehicle.Year
            };

            return returnVehicle;
        }

        public VehicleInventoryViewModel GetVehicleInventoryReport()
        {
            VehicleInventoryViewModel report = new VehicleInventoryViewModel();
            report.NewInventory = new List<InventoryReport>();
            report.UsedInventory = new List<InventoryReport>();

            MakeRepoMock makeRepo = new MakeRepoMock();
            var makes = makeRepo.GetAll();

            ModelRepoMock modelRepo = new ModelRepoMock();
            var models = modelRepo.GetAll();
            foreach (var v in _vehicles)
            {
                InventoryReport r = new InventoryReport()
                {
                    Count = 1,
                    MakeName = makes.FirstOrDefault(x => x.MakeId == v.MakeId).Name,
                    ModelName = models.FirstOrDefault(x => x.ModelId == v.ModelId).ModelName,
                    TotalStock = v.SalePrice,
                    Year = v.Year
                };
                if(v.Type == "New")
                {
                    report.NewInventory.Add(r);
                }
                else
                {
                    report.UsedInventory.Add(r);
                }
            }
            return report;
        }

        public void RemoveFeatured(int vehicleId)
        {
            _features.RemoveAll(x => x.VehicleId == vehicleId);
        }

        public Vehicle Update(Vehicle vehicle)
        {
            _vehicles.RemoveAll(x => x.VehicleId == vehicle.VehicleId);
            _vehicles.Add(vehicle);
            return vehicle;
        }

        private int GetNextId()
        {
            return _vehicles.Max(x => x.VehicleId) + 1;
        }
        private int GetNextFeatureId()
        {
            return _features.Max(x => x.FeaturedId) + 1;
        }

        public IEnumerable<VehicleViewModel> GetNewVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            List<VehicleViewModel> foundVehicles = new List<VehicleViewModel>();
            MakeRepoMock makeRepo = new MakeRepoMock();
            var makes = makeRepo.GetAll();
            ModelRepoMock modelRepo = new ModelRepoMock();
            var models = modelRepo.GetAll();
            foreach (var v in _vehicles)
            {
                VehicleViewModel model = new VehicleViewModel()
                {
                    Make = makes.FirstOrDefault(x => x.MakeId == v.MakeId).Name,
                    Model = models.FirstOrDefault(x => x.ModelId == v.ModelId).ModelName
                };
                if (v.Year >= minYear && v.Year <= maxYear)
                {
                    if (v.SalePrice >= minPrice && v.SalePrice <= maxPrice)
                    {
                        if (model.Model.Contains(searchText) || model.Make.Contains(searchText))
                        {
                            if (v.Type == "New")
                            {
                                model.Interior = v.Interior;
                                model.Mileage = v.Mileage;
                                model.MSRP = v.MSRP;
                                model.SalePrice = v.SalePrice;
                                model.Sold = v.Sold;
                                model.Style = v.Style;
                                model.Transmission = v.Transmission;
                                model.Type = v.Type;
                                model.VehicleId = v.VehicleId;
                                model.Vin = v.Vin;
                                model.Year = v.Year;
                                foundVehicles.Add(model);
                            }
                        }
                    }
                }
            }
            return foundVehicles;
        }

        public IEnumerable<VehicleViewModel> GetUsedVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            List<VehicleViewModel> foundVehicles = new List<VehicleViewModel>();
            MakeRepoMock makeRepo = new MakeRepoMock();
            var makes = makeRepo.GetAll();
            ModelRepoMock modelRepo = new ModelRepoMock();
            var models = modelRepo.GetAll();
            foreach (var v in _vehicles)
            {
                VehicleViewModel model = new VehicleViewModel()
                {
                    Make = makes.FirstOrDefault(x => x.MakeId == v.MakeId).Name,
                    Model = models.FirstOrDefault(x => x.ModelId == v.ModelId).ModelName
                };
                if (v.Year >= minYear && v.Year <= maxYear)
                {
                    if (v.SalePrice >= minPrice && v.SalePrice <= maxPrice)
                    {
                        if (model.Model.Contains(searchText) || model.Make.Contains(searchText))
                        {
                            if (v.Type == "Used")
                            {
                                model.Interior = v.Interior;
                                model.Mileage = v.Mileage;
                                model.MSRP = v.MSRP;
                                model.SalePrice = v.SalePrice;
                                model.Sold = v.Sold;
                                model.Style = v.Style;
                                model.Transmission = v.Transmission;
                                model.Type = v.Type;
                                model.VehicleId = v.VehicleId;
                                model.Vin = v.Vin;
                                model.Year = v.Year;
                                foundVehicles.Add(model);
                            }
                        }
                    }
                }
            }
            return foundVehicles;
        }

        public IEnumerable<VehicleViewModel> GetAllVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100)
        {
            if (searchText == null)
            {
                searchText = "";
            }
            List<VehicleViewModel> foundVehicles = new List<VehicleViewModel>();
            MakeRepoMock makeRepo = new MakeRepoMock();
            var makes = makeRepo.GetAll();
            ModelRepoMock modelRepo = new ModelRepoMock();
            var models = modelRepo.GetAll();
            foreach (var v in _vehicles)
            {
                VehicleViewModel model = new VehicleViewModel()
                {
                    Make = makes.FirstOrDefault(x => x.MakeId == v.MakeId).Name,
                    Model = models.FirstOrDefault(x => x.ModelId == v.ModelId).ModelName
                };
                if (v.Year >= minYear && v.Year <= maxYear)
                {
                    if (v.SalePrice >= minPrice && v.SalePrice <= maxPrice)
                    {
                        if (model.Model.Contains(searchText) || model.Make.Contains(searchText))
                        {
                            model.Interior = v.Interior;
                            model.Mileage = v.Mileage;
                            model.MSRP = v.MSRP;
                            model.SalePrice = v.SalePrice;
                            model.Sold = v.Sold;
                            model.Style = v.Style;
                            model.Transmission = v.Transmission;
                            model.Type = v.Type;
                            model.VehicleId = v.VehicleId;
                            model.Vin = v.Vin;
                            model.Year = v.Year;
                            foundVehicles.Add(model);
                        }
                    }
                }
            }
            return foundVehicles;
        }

        public void ChangeToSold(int id)
        {
            var found = _vehicles.FirstOrDefault(x => x.VehicleId == id);
            found.Sold = true;
            _vehicles.RemoveAll(x => x.VehicleId == id);
            _vehicles.Add(found);
        }
    }
}
