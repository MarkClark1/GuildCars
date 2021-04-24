using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.PageViewModels
{
    public class HomeIndexViewModel
    {
        public FeaturedVehiclesViewModel featuredVehicles { get; set; }
        public List<Special> specials { get; set; }
    }
}
