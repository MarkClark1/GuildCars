using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using GuildCars.Models.PageViewModels;

namespace GuildCars.UI.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public ActionResult Logout()
        {
            var auth = HttpContext.GetOwinContext().Authentication;
            auth.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;

            // attempt to load the user with this password
            AppUser user = userManager.Find(model.UserName, model.Password);

            // user will be null if the password or user name is bad
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return View(model);
            }
            else
            {
                // successful login, set up their cookies and send them on their way
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(returnUrl))
                    return RedirectToAction(returnUrl);
                else
                    return RedirectToAction("Index","Home");
            }
        }

        [Authorize()]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new AccountChangePasswordViewModel();
            var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            model.Id = User.Identity.GetUserId();
            return View(model);
        }

        [Authorize()]
        [HttpPost]
        public ActionResult ChangePassword(AccountChangePasswordViewModel model)
        {
            if(model.ConfirmPassword != model.Password)
            {
                ModelState.AddModelError("", "confirm password did not match");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var userMgr = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
                var response =  userMgr.ChangePassword(User.Identity.GetUserId(), model.OldPassword, model.ConfirmPassword);
                if (response.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach(string error in response.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
             return View(model);
            
            
        }
    }
}