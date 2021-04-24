using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GuildCars.Models.PageViewModels
{
    public class AdminEditVehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool Delete { get; set; }
        public bool Feature { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
    }
}
