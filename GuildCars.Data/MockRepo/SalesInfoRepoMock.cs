using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Queries.Reports;
using GuildCars.Models.Tables;

namespace GuildCars.Data.MockRepo
{
    public class SalesInfoRepoMock : ISalesInfoRepository
    {
        private static List<SaleInfo> _sales = new List<SaleInfo>()
        {

        };
        public SaleInfo Create(SaleInfo sale)
        {
            sale.SaleInfoId = GetNextId();
            var vehicleRepo = new VehicleRepoMock();
            vehicleRepo.ChangeToSold(sale.VehicleId);
            _sales.Add(sale);
            return sale;
        }

        public List<SaleReport> Sales(string userId = "0", DateTime? fromDate = null, DateTime? toDate = null)
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
            List<SaleReport> report = new List<SaleReport>();
            if (userId != "0")
            {
                var foundSales = _sales.Where(x => x.PurchaseDate > from).Where(x => x.PurchaseDate < to).ToList();
                foreach (var s in foundSales)
                {
                    SaleReport current = new SaleReport()
                    {
                        FullName = s.Name,
                        TotalVehiclesSold = _sales.Count(x => x.UserId == s.UserId),
                        TotalSalesAmount = _sales.Where(x => x.UserId == s.UserId).Sum(x => x.PurchasePrice)
                    };
                    report.Add(current);
                }
            }
            else
            {               
                var foundSales = _sales.Where(x => x.PurchaseDate > from).Where(x => x.PurchaseDate < to).ToList();
                foreach (var s in foundSales)
                {
                    SaleReport current = new SaleReport()
                    {
                        FullName = s.Name,
                        TotalVehiclesSold = _sales.Count(x => x.UserId == s.UserId),
                        TotalSalesAmount = _sales.Where(x => x.UserId == s.UserId).Sum(x => x.PurchasePrice)
                    };
                    report.Add(current);
                }
            }
            return report;
        }
        
        private int GetNextId()
        {
            if (_sales.Any())
                return _sales.Max(x => x.SaleInfoId) + 1;
            else
                return 1;           
        }
    }
}
