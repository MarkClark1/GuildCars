using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;
using GuildCars.Models.Tables;
using System.IO;
using GuildCars.Models.PageViewModels;
using GuildCars.Data.Interfaces;
using GuildCars.Data.Factories;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Vehicles()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            if(model.Vehicle.Year > 2020 || model.Vehicle.Year < 1769)
            {
                ModelState.AddModelError("error", "No vehicles currently exist out side of the years 1769-2020");
                return View(model);
            }
            if(model.Vehicle.ModelId == 0 || model.Vehicle.MakeId == 0)
            {
                ModelState.AddModelError("error", "Please provide a make and model");
                return View(model);
            }
            if(ModelState.IsValid)
            {
                IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
                var vehicle = repo.Create(model.Vehicle);

                byte[] imageByte = null;
                BinaryReader rdr = new BinaryReader(model.Image.InputStream);
                imageByte = rdr.ReadBytes((int)model.Image.ContentLength);
                if (System.IO.File.Exists(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" + model.Vehicle.VehicleId + ".jpg"))
                {
                    System.IO.File.Delete(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" + model.Vehicle.VehicleId + ".jpg");
                }
                using (FileStream fs = new FileStream(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" +vehicle.VehicleId+".jpg", FileMode.Create))
                {
                    fs.Write(imageByte, 0, imageByte.Length);
                }
                return RedirectToAction("EditVehicle/" + vehicle.VehicleId);
            }
            return View(model);
       
        }

        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            var vehicle = repo.GetById(id);
        
            AdminEditVehicleViewModel model = new AdminEditVehicleViewModel()
            {
                ModelName = vehicle.Model,
                MakeName = vehicle.Make,
                Vehicle = new Vehicle()
                {
                    Color = vehicle.Color,
                    Description = vehicle.Description,
                    Interior = vehicle.Interior,              
                    Mileage = vehicle.Mileage,
                    MSRP = vehicle.MSRP,
                    SalePrice = vehicle.SalePrice,
                    Style = vehicle.Style,
                    Transmission = vehicle.Transmission,
                    VehicleId = vehicle.VehicleId,
                    Type = vehicle.Type,
                    Vin = vehicle.Vin,
                    Year = vehicle.Year
                }          
                
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditVehicle(AdminEditVehicleViewModel model)
        {
            IVehicleRepository repo = RepoFactory.CreateVehicleRepo();
            if (model.Delete)
            {
                repo.RemoveFeatured(model.Vehicle.VehicleId);
                repo.Delete(model.Vehicle.VehicleId);
                return RedirectToAction("Index");
            }
            if (model.Vehicle.Year > 2020 || model.Vehicle.Year < 1769)
            {
                ModelState.AddModelError("error", "No vehicles currently exist out side of the years 1769-2020");
                return View(model);
            }
            if (model.Vehicle.ModelId == 0 || model.Vehicle.MakeId == 0)
            {
                ModelState.AddModelError("error","Please Provide a make and model");
                return View(model);
            }
            if (ModelState.IsValid)
            {                
                repo.Update(model.Vehicle);
                if(model.Image != null)
                {
                    byte[] imageByte = null;
                    BinaryReader rdr = new BinaryReader(model.Image.InputStream);
                    imageByte = rdr.ReadBytes((int)model.Image.ContentLength);
                    if (System.IO.File.Exists(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" + model.Vehicle.VehicleId + ".jpg"))
                    {
                        System.IO.File.Delete(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" + model.Vehicle.VehicleId + ".jpg");
                    }
                    using (FileStream fs = new FileStream(@"C:\Cohort\Repos\isaiah-dahlberg-individual-work\GuildCars\GuildCars\GuildCars.UI\Images\Inventory-" + model.Vehicle.VehicleId + ".jpg", FileMode.Create))
                    {
                        fs.Write(imageByte, 0, imageByte.Length);
                    }
                }
                if (model.Feature)
                {
                    repo.RemoveFeatured(model.Vehicle.VehicleId);
                    repo.AddFeatured(model.Vehicle.VehicleId);
                }
                else
                {
                    repo.RemoveFeatured(model.Vehicle.VehicleId);
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Users()
        {
            IUserRepository repo = RepoFactory.CreateUserRepo();
            var model = repo.GetAll().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            AddUserViewModel model = new AddUserViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddUser(AddUserViewModel model)
        {
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var user = new AppUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            bool success = userMgr.Create(user, model.Password).Succeeded;
            Claim lastName = new Claim("LastName", model.LastName);
            Claim firstName = new Claim("FirstName", model.FirstName);
            userMgr.AddClaim(user.Id, lastName);
            userMgr.AddClaim(user.Id, firstName);

            userMgr.AddToRole(user.Id, model.Role);
            if (success)
            {
                return RedirectToAction("Users");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            if(id == null)
            {
                return RedirectToAction("Users");
            }
            IUserRepository repo = RepoFactory.CreateUserRepo();
            var user = repo.GetUserById(id);
            var model = new AdminEditUserViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = User.Identity.GetUserId(),
                Role = user.Role,
                LastName = user.LastName
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditUser(AdminEditUserViewModel modelUser)
        {
            if (modelUser.ConfirmPassword != modelUser.Password)
            {
                ModelState.AddModelError("", "confirm password did not match");
                return View(modelUser);
            }
            if (ModelState.IsValid)
            {
                var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                AppUser newUser = new AppUser()
                {
                    Id = modelUser.Id,
                    Email = modelUser.Email,
                    UserName = modelUser.Email
                };
                try
                {
                    userMgr.Update(newUser);
                }
                catch { }
               
                var identity = new ClaimsIdentity(newUser.Id);

                IUserRepository repo = RepoFactory.CreateUserRepo();

                repo.UpDateFirstLastName(modelUser.LastName, modelUser.FirstName, modelUser.Id);
               
                var response = userMgr.ChangePassword(modelUser.Id, modelUser.OldPassword, modelUser.ConfirmPassword);
                if (response.Succeeded)
                {
                    return RedirectToAction("Users", "Admin");
                }
                foreach (string error in response.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(modelUser);
           
        }

        [HttpPost]
        public ActionResult Makes(string MakeInput)
        {
            IMakeRepository repo = RepoFactory.CreateMakeRepo();
            if(MakeInput != null)
            {
                var make = new Make()
                {
                    DateAdded = DateTime.Today,
                    Name = MakeInput,
                    UserId = System.Web.HttpContext.Current.User.Identity.GetUserId()
                };
                repo.Create(make);
            }
         

            AdminMakeViewModel model = new AdminMakeViewModel();
            model.Makes = repo.GetAll().ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult Makes()
        {
            IMakeRepository repo = RepoFactory.CreateMakeRepo();
            AdminMakeViewModel model = new AdminMakeViewModel();

            model.Makes = repo.GetAll().ToList();
            
 
            return View(model);
        }

        [HttpGet]
        public ActionResult Models()
        {
            IModelRepository repo = RepoFactory.CreateModelRepo();
            IMakeRepository makerepo = RepoFactory.CreateMakeRepo();

            AdminModelViewModel model = new AdminModelViewModel();
            model.Models = repo.GetAll().ToList();
            model.Makes = makerepo.GetAll().ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult Models(string ModelName, int MakeId)
        {
            IModelRepository repo = RepoFactory.CreateModelRepo();
            IMakeRepository makerepo = RepoFactory.CreateMakeRepo();

            if (ModelName != null && MakeId != 0)
            {
                var model = new Model()
                {
                    DateAdded = DateTime.Today,
                    Name = ModelName,
                    MakeId = MakeId,
                    UserId = System.Web.HttpContext.Current.User.Identity.GetUserId()
                };
                repo.Create(model);
            }

            AdminModelViewModel viewModel = new AdminModelViewModel();
            viewModel.Models = repo.GetAll().ToList();
            viewModel.Makes = makerepo.GetAll().ToList();

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Specials()
        {
            ISpecialRepository repo = RepoFactory.CreateSpecialRepo();

            AdminSpecialViewModel model = new AdminSpecialViewModel()
            {
                Specials = repo.GetAll().ToList()
            };
            return View(model);
        }
    
        [Route("Admin/SpecialDelete/{id}")]
        [HttpGet]
        public ActionResult DeleteSpecial(int id)
        {
            ISpecialRepository repo = RepoFactory.CreateSpecialRepo();
            repo.Delete(id);
            return RedirectToAction("Specials");
        }

        [HttpPost]
        public ActionResult Specials(AdminSpecialViewModel model)
        {
            if (ModelState.IsValid)
            {
                ISpecialRepository repo = RepoFactory.CreateSpecialRepo();
                Special special = new Special()
                {
                    Description = model.Description,
                    Title = model.Title
                };
                repo.Create(special);
                return RedirectToAction("Specials");
            }
            return View(model);
        }

    }
}