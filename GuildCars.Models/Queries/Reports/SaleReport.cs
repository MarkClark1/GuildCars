using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries.Reports
{
    public class SaleReport
    {
        public int TotalVehiclesSold { get; set; }
        public decimal TotalSalesAmount { get; set; }
        public string FullName { get; set; }
    }
}
