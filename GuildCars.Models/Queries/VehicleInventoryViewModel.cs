using GuildCars.Models.Queries.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleInventoryViewModel
    {
       public List<InventoryReport> NewInventory { get; set; }
       public List<InventoryReport> UsedInventory { get; set; }
    }
}
