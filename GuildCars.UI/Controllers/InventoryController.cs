using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.PageViewModels;
using GuildCars.UI.HelpMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class InventoryController : Controller
    {
       [HttpGet]
       public ActionResult New()
       {
            return View();
       }
        [HttpGet]
        public ActionResult Used()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var model = new VehicleImageViewModel();
            model.Vehicle  = repo.GetById(id);
            model.FilePath = Helpers.GetImageFilePath(id);

            return View(model);
        }
    }
}