namespace LoggingExercise.Migrations
{
    using LoggingExercise.DAL;
    using LoggingExercise.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LoggingExercise.DAL.DataStore>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LoggingExercise.DAL.DataStore context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            string[] roles = new string[2] { "ADMIN", "STAFF" };
            var roleStore = new RoleStore<AppRole, int, AppUserRole>(new DataStore());
            var roleManager = new RoleManager<AppRole, int>(roleStore);

            Array.ForEach(roles, r =>
            {
                if (roleManager.RoleExists(r))
                    return;

                roleManager.Create(new AppRole() { Name = r });
            });


            string username = "omolola@gail.com";
            string password = "admin";
            string role = "ADMIN";

            var userMgr = Startup.UserManagerFactory.Invoke();


            if (userMgr.FindByName(username) != null)
                return;


            var contact = new AppUser() { UserName = username };
            var result = userMgr.Create(contact, password);




            if (!roleManager.RoleExists(role))
            {
                var irole = new AppRole() { Name = role };
                roleManager.Create(irole);
            }

            if (!userMgr.IsInRole(contact.Id, role))
            {
                userMgr.AddToRole(contact.Id, role);
            }
        }
    
    }
}
