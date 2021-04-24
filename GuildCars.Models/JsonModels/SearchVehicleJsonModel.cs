using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.JsonModels
{
    public class SearchVehicleJsonModel
    {
        public int MinYear { get; set; }
        public int MaxYear { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string MakeModel { get; set; }
    }
}
