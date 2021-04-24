using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.PageViewModels;
using GuildCars.Models.Tables;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin, sale")]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            if(id == 0)
            {
                return RedirectToAction("Index");
            }
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            SalesPurchaseViewModel model = new SalesPurchaseViewModel();
            model.Vehicle = repo.GetById(id);
            model.ImageFilePath = HelpMethods.Helpers.GetImageFilePath(id);
            model.Sale = new SaleInfo()
            {
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId(),
                VehicleId = model.Vehicle.VehicleId,
                PurchaseDate = DateTime.Today

            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase(SalesPurchaseViewModel model)
        {
            if(model.Sale.PurchasePrice < (model.Vehicle.SalePrice * 0.95M) || model.Sale.PurchasePrice > model.Vehicle.MSRP)
            {
                ModelState.AddModelError("error", "The purchase price can not be less than 5% of the sale price or greater than the MSRP");
            }
            if (ModelState.IsValid)
            {
                ISalesInfoRepository repo = RepoFactory.CreateSaleInfoRepo();
                IVehicleRepository vehicleRepo = RepoFactory.CreateVehicleRepo();
                repo.Create(model.Sale);
                vehicleRepo.ChangeToSold(model.Sale.VehicleId);
                return RedirectToAction("Index");
            }
            IVehicleRepository vrepo = RepoFactory.CreateVehicleRepo();
            model.Vehicle = vrepo.GetById(model.Sale.VehicleId);
            model.ImageFilePath = HelpMethods.Helpers.GetImageFilePath(model.Sale.VehicleId);
            return View(model);
        }
    }
}