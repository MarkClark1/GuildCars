using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GuildCars.Models.PageViewModels
{
    public class AddVehicleViewModel
    {
        public Vehicle Vehicle { get; set; }
        [Required]
        public HttpPostedFileBase Image { get; set; }
    }
}
