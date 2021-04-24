using GuildCars.Models.Queries.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class FeaturedVehiclesViewModel
    {
        public FeaturedVehiclesViewModel()
        {
            features = new List<FeatureViewModel>();
        }
        public List<FeatureViewModel> features { get; set; }
    }
}
