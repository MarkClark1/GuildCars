using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.PageViewModels
{
    public class AdminMakeViewModel
    {
        public List<MakeViewModel> Makes { get; set; }
        public string MakeInput { get; set; }
    }
}
