using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Queries.Reports;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Repositories
{
    public class VehicleRepositoryADO : IVehicleRepository
    {
        private string _connection = Settings.GetConnectionString();

        public void AddFeatured(int vehicleId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.CommandText = "CreateFeatured";
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Vehicle Create(Vehicle vehicle)
        {
            if (string.IsNullOrEmpty(vehicle.Color) || string.IsNullOrEmpty(vehicle.Description) || string.IsNullOrEmpty(vehicle.Interior) ||
                vehicle.MakeId == 0 || vehicle.Mileage == 0 || vehicle.ModelId == 0 || string.IsNullOrEmpty(vehicle.Style)  || vehicle.SalePrice == 0 ||
                vehicle.MSRP == 0 || string.IsNullOrEmpty(vehicle.Transmission) || string.IsNullOrEmpty(vehicle.Vin) ||
                string.IsNullOrEmpty(vehicle.Type) || (vehicle.Year < 1600 || vehicle.Year > 2020))
            {
                return null;
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@VehicleId", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;

                cmd.CommandText = "CreateVehicle";
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Style", vehicle.Style);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@Trans", vehicle.Transmission);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@Interior", vehicle.Interior);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);

                int soldBit = vehicle.Sold ? 1 : 0;
                cmd.Parameters.AddWithValue("@Sold", soldBit);

                conn.Open();
                cmd.ExecuteNonQuery();
                vehicle.VehicleId = (int)param.Value;
            }
            return vehicle;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;             

                cmd.CommandText = "DeleteVehicle";
                cmd.Parameters.AddWithValue("@Id", id);     

                conn.Open();
                cmd.ExecuteNonQuery();
            }
          
        }

        public FeaturedVehiclesViewModel GetAllFeatures()
        {
            FeaturedVehiclesViewModel featured = new FeaturedVehiclesViewModel();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetAllFeatures";
                cmd.CommandType = CommandType.StoredProcedure;
 

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        FeatureViewModel current = new FeatureViewModel();

                        current.VehicleId = (int)dr["VehicleId"];
                        current.YearMakeModel = dr["YearMakeModel"].ToString();
                        current.SalePrice = (decimal)dr["SalePrice"];

                        featured.features.Add(current);
                    }
                }
            }

            return featured;
        }

        public VehicleViewModel GetById(int id)
        {
            VehicleViewModel vehicle = new VehicleViewModel();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetVehicleById";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleViewModel current = new VehicleViewModel();

                        current.VehicleId = (int)dr["VehicleId"];
                        current.Year = (int)dr["Year"];
                        current.Type = dr["Type"].ToString();
                        current.Style = dr["Style"].ToString();
                        current.Transmission = dr["Trans"].ToString();
                        current.Color = dr["Color"].ToString();
                        current.Interior = dr["Interior"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.Vin = dr["Vin"].ToString();
                        current.SalePrice = (decimal)dr["SalePrice"];
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Sold = (bool)dr["Sold"];
                        current.Description = dr["Description"].ToString();
                        current.Make = dr["MakeName"].ToString();
                        current.Model = dr["ModelName"].ToString();

                        vehicle = current;
                    }
                }
            }
            return vehicle;
        }

        public VehicleInventoryViewModel GetVehicleInventoryReport()
        {
            VehicleInventoryViewModel report = new VehicleInventoryViewModel();
            report.UsedInventory = new List<InventoryReport>();
            report.NewInventory = new List<InventoryReport>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetUsedInventoryReport";
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReport current = new InventoryReport();

                        current.Count = (int)dr["Count"];
                        current.Year = (int)dr["Year"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.ModelName = dr["ModelName"].ToString();
                        current.TotalStock = (decimal)dr["Stock"];

                        report.UsedInventory.Add(current);
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetNewInventoryReport";
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReport current = new InventoryReport();

                        current.Count = (int)dr["Count"];
                        current.Year = (int)dr["Year"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.ModelName = dr["ModelName"].ToString();
                        current.TotalStock = (decimal)dr["Stock"];

                        report.NewInventory.Add(current);
                    }
                }
            }
            return report;
        }

        public IEnumerable<VehicleViewModel> GetNewVehiclesSorted(string searchText = "", decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2500)
        {
            List<VehicleViewModel> vehicles = new List<VehicleViewModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetNewVehiclesSorted";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchText", searchText);
                cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@MinYear", minYear);
                cmd.Parameters.AddWithValue("@MaxYear", maxYear);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleViewModel current = new VehicleViewModel();

                        current.VehicleId = (int)dr["VehicleId"];
                        current.Year = (int)dr["Year"];
                        current.Type = dr["Type"].ToString();
                        current.Style = dr["Style"].ToString();
                        current.Transmission = dr["Trans"].ToString();
                        current.Color = dr["Color"].ToString();
                        current.Interior = dr["Interior"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.Vin = dr["Vin"].ToString();
                        current.SalePrice = (decimal)dr["SalePrice"];
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Sold = (bool)dr["Sold"];
                        current.Description = dr["Description"].ToString();
                        current.Make = dr["MakeName"].ToString();
                        current.Model = dr["ModelName"].ToString();

                        vehicles.Add(current);
                    }
                }
            }

            return vehicles;
        }

        public IEnumerable<VehicleViewModel> GetUsedVehiclesSorted(string searchText = "", decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2500)
        {
            List<VehicleViewModel> vehicles = new List<VehicleViewModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetUsedVehiclesSorted";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchText", searchText);
                cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@MinYear", minYear);
                cmd.Parameters.AddWithValue("@MaxYear", maxYear);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleViewModel current = new VehicleViewModel();

                        current.VehicleId = (int)dr["VehicleId"];
                        current.Year = (int)dr["Year"];
                        current.Type = dr["Type"].ToString();
                        current.Style = dr["Style"].ToString();
                        current.Transmission = dr["Trans"].ToString();
                        current.Color = dr["Color"].ToString();
                        current.Interior = dr["Interior"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.Vin = dr["Vin"].ToString();
                        current.SalePrice = (decimal)dr["SalePrice"];
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Sold = (bool)dr["Sold"];
                        current.Description = dr["Description"].ToString();
                        current.Make = dr["MakeName"].ToString();
                        current.Model = dr["ModelName"].ToString();

                        vehicles.Add(current);
                    }
                }
            }

            return vehicles;
        }

        public void RemoveFeatured(int vehicleId)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.CommandText = "DeleteFeatured";
                cmd.Parameters.AddWithValue("@VehicleId", vehicleId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Vehicle Update(Vehicle vehicle)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;                

                cmd.CommandText = "UpdateVehicle";
                cmd.Parameters.AddWithValue("@VehicleId", vehicle.VehicleId);
                cmd.Parameters.AddWithValue("@Year", vehicle.Year);
                cmd.Parameters.AddWithValue("@Type", vehicle.Type);
                cmd.Parameters.AddWithValue("@Style", vehicle.Style);
                cmd.Parameters.AddWithValue("@ModelId", vehicle.ModelId);
                cmd.Parameters.AddWithValue("@MakeId", vehicle.MakeId);
                cmd.Parameters.AddWithValue("@Trans", vehicle.Transmission);
                cmd.Parameters.AddWithValue("@Color", vehicle.Color);
                cmd.Parameters.AddWithValue("@Interior", vehicle.Interior);
                cmd.Parameters.AddWithValue("@Mileage", vehicle.Mileage);
                cmd.Parameters.AddWithValue("@Vin", vehicle.Vin);
                cmd.Parameters.AddWithValue("@SalePrice", vehicle.SalePrice);
                cmd.Parameters.AddWithValue("@MSRP", vehicle.MSRP);
                cmd.Parameters.AddWithValue("@Description", vehicle.Description);

                int soldBit = vehicle.Sold ? 1 : 0;
                cmd.Parameters.AddWithValue("@Sold", soldBit);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return vehicle;
        }

        public void ChangeToSold(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ChangeVehicleToSold";
                cmd.Parameters.AddWithValue("@Id", id);         

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<VehicleViewModel> GetAllVehiclesSorted(string searchText = null, decimal minPrice = 0, decimal maxPrice = 900000000, int minYear = 0, int maxYear = 2100)
        {
            List<VehicleViewModel> vehicles = new List<VehicleViewModel>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "GetAllVehiclesSorted";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SearchText", searchText);
                cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                cmd.Parameters.AddWithValue("@MinYear", minYear);
                cmd.Parameters.AddWithValue("@MaxYear", maxYear);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        VehicleViewModel current = new VehicleViewModel();

                        current.VehicleId = (int)dr["VehicleId"];
                        current.Year = (int)dr["Year"];
                        current.Type = dr["Type"].ToString();
                        current.Style = dr["Style"].ToString();
                        current.Transmission = dr["Trans"].ToString();
                        current.Color = dr["Color"].ToString();
                        current.Interior = dr["Interior"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.Vin = dr["Vin"].ToString();
                        current.SalePrice = (decimal)dr["SalePrice"];
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Sold = (bool)dr["Sold"];
                        current.Description = dr["Description"].ToString();
                        current.Make = dr["MakeName"].ToString();
                        current.Model = dr["ModelName"].ToString();

                        vehicles.Add(current);
                    }
                }
            }

            return vehicles;
        }
    }
}
