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
    public class SalesInfoRepositoryADO : ISalesInfoRepository
    {
        private string _connection = Settings.GetConnectionString();
        public SaleInfo Create(SaleInfo sale)
        {
            if(string.IsNullOrEmpty(sale.City) || string.IsNullOrEmpty(sale.Email) || string.IsNullOrEmpty(sale.Name) ||
                string.IsNullOrEmpty(sale.Phone) || (sale.PurchasePrice == 0) || string.IsNullOrEmpty(sale.PurchaseType) ||
                string.IsNullOrEmpty(sale.State) || string.IsNullOrEmpty(sale.StreetOne) || string.IsNullOrEmpty(sale.UserId) ||
                (sale.ZipCode > 99999 || sale.ZipCode < 10000) || sale.VehicleId == 0)
            {
                return null;
            }
            if (sale.StreetTwo == null)
            {
                sale.StreetTwo = "";
            }
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _connection;

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "CreateSaleInfo";

                    cmd.Parameters.AddWithValue("@Name", sale.Name);
                    cmd.Parameters.AddWithValue("@Email", sale.Email);
                    cmd.Parameters.AddWithValue("@Phone", sale.Phone);
                    cmd.Parameters.AddWithValue("@StreetOne", sale.StreetOne);
                    cmd.Parameters.AddWithValue("@StreetTwo", sale.StreetTwo);
                    cmd.Parameters.AddWithValue("@State", sale.State);
                    cmd.Parameters.AddWithValue("@City", sale.City);
                    cmd.Parameters.AddWithValue("@ZipCode", sale.ZipCode);
                    cmd.Parameters.AddWithValue("@VehicleId", sale.VehicleId);
                    cmd.Parameters.AddWithValue("@UserId", sale.UserId);
                    cmd.Parameters.AddWithValue("@PurchasePrice", sale.PurchasePrice);
                    cmd.Parameters.AddWithValue("@PurchaseDate", sale.PurchaseDate);
                    cmd.Parameters.AddWithValue("@PurchaseType", sale.PurchaseType);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return sale;
            }
            catch
            {
                return null;
            }
           
        }

        public List<SaleReport> Sales(string userId, DateTime? fromDate, DateTime? toDate)
        {
            DateTime from;
            DateTime to;
            if (fromDate <= new DateTime(1 / 1 / 0001))
            {
                from = new DateTime(1899, 1, 1);
            }
            else
            {
                from = (DateTime)fromDate;
            }

            if (toDate <= new DateTime(1 / 1 / 0001))
            {
                to = new DateTime(2199, 1, 1);
            }
            else
            {
                    to = (DateTime)toDate;
            }

            //DateTime from = fromDate ?? new DateTime(1899, 1, 1);
            //DateTime to = toDate ?? new DateTime(2299, 1, 1);

            List<SaleReport> reports = new List<SaleReport>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _connection;

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (userId != "")
                {
                    cmd.CommandText = "GetSalesReportOfSpecific";
                    cmd.Parameters.AddWithValue("@Id", userId);
                }
                else
                {
                    cmd.CommandText = "GetSalesReportOfAll";
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FromDate", from);
                cmd.Parameters.AddWithValue("@ToDate", to);

                conn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SaleReport current = new SaleReport();

                        current.FullName = dr["FullName"].ToString();
                        current.TotalSalesAmount = (decimal)dr["TotalAmount"];
                        current.TotalVehiclesSold = (int)dr["Count"];

                       reports.Add(current);
                    }
                }
            }
            return reports;
        }
    }
}