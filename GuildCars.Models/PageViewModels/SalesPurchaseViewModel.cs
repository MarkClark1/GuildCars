using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.PageViewModels
{
    public class SalesPurchaseViewModel
    {
        public SaleInfo Sale { get; set; }
        public VehicleViewModel Vehicle { get; set; }
        public string ImageFilePath { get; set; }
    }
}
