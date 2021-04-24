using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries.Reports
{
    public class FeatureViewModel
    {
        public string YearMakeModel { get; set; }
        public decimal SalePrice { get; set; }
        public string VehicleImagePath { get; set; }
        public int VehicleId { get; set; }
    }
}
