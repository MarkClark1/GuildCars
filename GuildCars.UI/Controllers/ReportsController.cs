using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.PageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sales()
        {
            IUserRepository repo = RepoFactory.CreateUserRepo();
            ReportSalesViewModel model = new ReportSalesViewModel();
            model.users = repo.GetAll().ToList();
            return View(model);
        }

        public ActionResult Inventory()
        {
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var model = repo.GetVehicleInventoryReport();
            return View(model);
        }
    }
}