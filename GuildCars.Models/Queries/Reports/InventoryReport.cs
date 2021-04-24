using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries.Reports
{
    public class InventoryReport
    {
        public int Year { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public decimal TotalStock { get; set; }
        public int Count { get; set; }
    }
}
