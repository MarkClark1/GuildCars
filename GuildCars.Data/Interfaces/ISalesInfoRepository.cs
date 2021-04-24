using GuildCars.Models.Queries;
using GuildCars.Models.Queries.Reports;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface ISalesInfoRepository
    {
        SaleInfo Create(SaleInfo sale);
        List<SaleReport> Sales(string userId = "0", DateTime? fromDate = null, DateTime? toDate = null);
    }
}
