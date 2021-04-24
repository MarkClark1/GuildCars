namespace GuildCars.UI.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCars.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCars.UI.Models.ApplicationDbContext context)
        {
            // Load the user and role managers with our custom models
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            // have we loaded roles already?
            if (roleMgr.RoleExists("admin"))
                return;

            // create the admin role
            roleMgr.Create(new AppRole() { Name = "admin" });
            roleMgr.Create(new AppRole() { Name = "sales" });
            roleMgr.Create(new AppRole() { Name = "disabled" });
            // create the default user
            var user = new AppUser()
            {
                UserName = "admin"
            };

            // create the user with the manager class
            userMgr.Create(user, "testing123");

            // add the user to the admin role
            userMgr.AddToRole(user.Id, "admin");
        }
    }
}
