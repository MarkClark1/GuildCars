using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.PageViewModels
{
    public class AdminModelViewModel
    {
        public List<ModelViewModel> Models { get; set; }
        public int MakeId { get; set; }
        public string ModelName { get; set; }
        public List<MakeViewModel> Makes { get; set; }
    }
}
