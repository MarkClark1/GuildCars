using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.PageViewModels
{
    public class AdminSpecialViewModel
    {
        public List<Special> Specials { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
