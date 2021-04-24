using GuildCars.Data.Factories;
using GuildCars.Data.Interfaces;
using GuildCars.Models.PageViewModels;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.Tables;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ISpecialRepository special_repo = RepoFactory.CreateSpecialRepo();
            IVehicleRepository vehicle_repo = RepoFactory.CreateVehicleRepo();

        
            var indexView = new HomeIndexViewModel()
            {                
                specials = special_repo.GetAll().ToList(),
                featuredVehicles = vehicle_repo.GetAllFeatures()
            };
            foreach(var v in indexView.featuredVehicles.features)
            {
                v.VehicleImagePath = ImageFinder(v.VehicleId);
            }
            return View(indexView);
        }

        public ActionResult Specials()
        {
            ISpecialRepository repo = RepoFactory.CreateSpecialRepo();
            var model = repo.GetAll().ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult Contact(string id)
        {
            ContactRecord model = new ContactRecord();
            model.Message ="Vin #: " + id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Contact(ContactRecord model)
        {            
            if (model.Email == null && model.Phone == null)
            {
                ModelState.AddModelError("Error", "Either Provide an Email or phone");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                IContactRecordRepository repo = RepoFactory.CreateContactRecordRepo();
                repo.Create(model);
                ModelState.AddModelError("Error", "Your response has been submitted");
                return View(new ContactRecord());
            }
            else
            {
                return View(model);
            }
            
            
        }
    

        public static string ImageFinder(int id)
        { 
            return @"../Images/inventory-" + id + ".jpg";
        }
    }
}