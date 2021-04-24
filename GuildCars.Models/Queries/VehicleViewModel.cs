using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Transmission { get; set; }
        public string Color { get; set; }
        public string Interior { get; set; }
        public int Mileage { get; set; }
        public string Vin { get; set; }
        public decimal SalePrice { get; set; }
        public decimal MSRP { get; set; }
        public string Description { get; set; }
        public bool Sold { get; set; }
    }
}
