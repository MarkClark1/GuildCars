using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class ModelViewModel
    {
        public int ModelId { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public DateTime DateAdded { get; set; }
        public string EmailOfAdder { get; set; }
    }     
}
