using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using GuildCars.Data.Repositories;
using System.Data.SqlClient;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Test
{
    [TestFixture]
    public static class VehicleRepoADOTest
    {
        [TearDown]
        public static void TearDownDatabase()
        {
            using (var cn = new SqlConnection("Server = localhost; Database = GuildCars; Integrated Security = True;"))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "TearDownTestData";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [SetUp]
        public static void SetUpTestDatabase()
        {
            using (var cn = new SqlConnection("Server = localhost; Database = GuildCars; Integrated Security = True;"))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "RestTestData";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public static void CanGetVehicleById()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            var vehicle = repo.GetById(3);

            Assert.AreEqual(3, vehicle.VehicleId);
            Assert.AreEqual(2019, vehicle.Year);
            Assert.AreEqual("Ford", vehicle.Make);
            Assert.AreEqual("Mustang", vehicle.Model);
        }

        [Test]
        public static void CanCreateVehicle()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreNotEqual(null, createdvehicle);
            var resultId = createdvehicle.VehicleId;
            var resultVehicle = repo.GetById(resultId);

            Assert.GreaterOrEqual(resultVehicle.VehicleId, 1);
            Assert.AreEqual(vehicle.Vin, resultVehicle.Vin);
            Assert.AreEqual(vehicle.Color, resultVehicle.Color);
            Assert.AreEqual(vehicle.Style, resultVehicle.Style);
            Assert.AreEqual(vehicle.Description, resultVehicle.Description);
            Assert.AreEqual(vehicle.Interior, resultVehicle.Interior);
            Assert.AreEqual("Subaru", resultVehicle.Make);
            Assert.AreEqual("Impreza", resultVehicle.Model);
            Assert.AreEqual(vehicle.MSRP, resultVehicle.MSRP);
            Assert.AreEqual(vehicle.Mileage, resultVehicle.Mileage);
            Assert.AreEqual(vehicle.SalePrice, resultVehicle.SalePrice);
            Assert.AreEqual(vehicle.Sold, resultVehicle.Sold);
            Assert.AreEqual(vehicle.Transmission, resultVehicle.Transmission);
            Assert.AreEqual(vehicle.Type, resultVehicle.Type);
            Assert.AreEqual(vehicle.Year, resultVehicle.Year);
        }

        [Test]
        public static void zCanCreateFeature()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();
            var before = repo.GetAllFeatures().features.Count();
            repo.AddFeatured(1);
            var after = repo.GetAllFeatures().features.Count();
            Assert.AreEqual(before, after - 1);
        }

        [Test]
        public static void zCanGetFeatures()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();
            var result =  repo.GetAllFeatures();

            Assert.GreaterOrEqual(1, result.features.Count);
        }

        [Test]
        public static void zCanDeleteFeature()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();
            var before = repo.GetAllFeatures().features.Count();
            repo.RemoveFeatured(1);
            var after = repo.GetAllFeatures().features.Count();
            Assert.AreEqual(before, after + 1);
        }

        [Test]
        public static void CanGetSortedVehiclesWithoutParams()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            List<VehicleViewModel> vehicles = repo.GetAllVehiclesSorted("").ToList();

            Assert.AreEqual(1, vehicles[0].VehicleId);
            Assert.AreEqual("Impreza", vehicles[0].Model);
            Assert.AreEqual("Subaru", vehicles[0].Make);
            Assert.AreEqual(2, vehicles[1].VehicleId);
            Assert.AreEqual("Focus", vehicles[1].Model);
            Assert.AreEqual("Ford", vehicles[1].Make);
            Assert.AreEqual(3, vehicles[2].VehicleId);
            Assert.AreEqual("Mustang", vehicles[2].Model);
            Assert.AreEqual("Ford", vehicles[2].Make);
        }

        [Test]
        public static void CanGetSortedVehiclesWithParamsOfYear()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            List<VehicleViewModel> vehicles = repo.GetAllVehiclesSorted("",minYear: 2014,maxYear: 2016).ToList();

            Assert.AreEqual(1, vehicles.Count);
            Assert.AreEqual(1, vehicles[0].VehicleId);
            Assert.AreEqual("Impreza", vehicles[0].Model);
            Assert.AreEqual("Subaru", vehicles[0].Make);
        }

        [Test]
        public static void CanGetSortedVehiclesWithParamsOfPrice()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            List<VehicleViewModel> vehicles = repo.GetAllVehiclesSorted("",minPrice: 11000M, maxPrice: 15500M).ToList();

            Assert.AreEqual(2, vehicles.Count);
            Assert.AreEqual(1, vehicles[0].VehicleId);
            Assert.AreEqual("Impreza", vehicles[0].Model);
            Assert.AreEqual("Subaru", vehicles[0].Make);
            Assert.AreEqual(2, vehicles[1].VehicleId);
            Assert.AreEqual("Focus", vehicles[1].Model);
            Assert.AreEqual("Ford", vehicles[1].Make);
        }


        [Test]
        public static void CanGetSortedVehiclesWithParamsOfModelAndMake()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            List<VehicleViewModel> vehicles = repo.GetAllVehiclesSorted(searchText: "a").ToList();

            Assert.AreEqual(3, vehicles.Count);
            Assert.AreEqual(1, vehicles[0].VehicleId);
            Assert.AreEqual("Impreza", vehicles[0].Model);
            Assert.AreEqual("Subaru", vehicles[0].Make);
            Assert.AreEqual(3, vehicles[1].VehicleId);
            Assert.AreEqual("Mustang", vehicles[1].Model);
            Assert.AreEqual("Ford", vehicles[1].Make);
        }

        [Test]
        public static void CanUpdateVehicle()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                VehicleId = 1,
                Vin = "0000000000",
                Color = "Blue",
                Style = "Car",
                Description = "Notes and stuff",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var editedvehicle = repo.Update(vehicle);
            var resultInt = editedvehicle.VehicleId;
            var resultVehicle = repo.GetById(resultInt);

            Assert.AreEqual(resultVehicle.VehicleId, 1);
            Assert.AreEqual(vehicle.Vin, resultVehicle.Vin);
            Assert.AreEqual(vehicle.Color, resultVehicle.Color);
            Assert.AreEqual(vehicle.Style, resultVehicle.Style);
            Assert.AreEqual(vehicle.Description, resultVehicle.Description);
            Assert.AreEqual(vehicle.Interior, resultVehicle.Interior);
            Assert.AreEqual("Subaru", resultVehicle.Make);
            Assert.AreEqual("Impreza", resultVehicle.Model);
            Assert.AreEqual(vehicle.MSRP, resultVehicle.MSRP);
            Assert.AreEqual(vehicle.Mileage, resultVehicle.Mileage);
            Assert.AreEqual(vehicle.SalePrice, resultVehicle.SalePrice);
            Assert.AreEqual(vehicle.Sold, resultVehicle.Sold);
            Assert.AreEqual(vehicle.Transmission, resultVehicle.Transmission);
            Assert.AreEqual(vehicle.Type, resultVehicle.Type);
            Assert.AreEqual(vehicle.Year, resultVehicle.Year);
        }

        [Test]
        public static void CanCreateAndDeleteVehicle()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Interior = "Black",
                Style = "Car",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreNotEqual(null, createdvehicle);
            var resultId = createdvehicle.VehicleId;
            repo.Delete(resultId);
            var deletedVehicle = repo.GetById(resultId);

            Assert.AreEqual(null, deletedVehicle.Vin);
        }

        [Test]
        public static void CanFailCreateVehicle1()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);            
        }

        [Test]
        public static void CanFailCreateVehicle2()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle3()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle4()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle5()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle6()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 0,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle7()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 0,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle8()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 0M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle9()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 0M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle10()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "",
                Type = "New",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle11()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "",
                Year = 2015
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }

        [Test]
        public static void CanFailCreateVehicle12()
        {
            VehicleRepositoryADO repo = new VehicleRepositoryADO();

            Vehicle vehicle = new Vehicle()
            {
                Vin = "0000000000",
                Color = "Blue",
                Description = "Notes and stuff",
                Style = "Car",
                Interior = "Black",
                MakeId = 1,
                ModelId = 1,
                Mileage = 500,
                MSRP = 15000M,
                SalePrice = 12000M,
                Sold = false,
                Transmission = "Auto",
                Type = "New",
                Year = 2050
            };

            var createdvehicle = repo.Create(vehicle);
            Assert.AreEqual(null, createdvehicle);
        }
    }
}
