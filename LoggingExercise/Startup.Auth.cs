using LoggingExercise.DAL;
using LoggingExercise.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoggingExercise
{
    public partial class Startup
    {
        public static Func<UserManager<AppUser, int>> UserManagerFactory { get; private set; } = Create;
        public static Func<RoleManager<AppRole, int>> RoleManagerFactory { get; private set; } = RoleCreate;

        public static void ConfigureAuth(IAppBuilder app)
        {

            var option = new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                CookieName = "Redirecting",
                LoginPath = new PathString("/Auth/Login"),

            };
            app.UseCookieAuthentication(option);
        }


        public static UserManager<AppUser, int> Create()

        {

            var dbContext = new DataStore();
            var store = new UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>(dbContext);
            var usermanager = new UserManager<AppUser, int>(store);
            // allow alphanumeric characters in username
            usermanager.UserValidator = new UserValidator<AppUser, int>(usermanager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false,
            };

            usermanager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 4,
                RequireDigit = false,
                RequireUppercase = false
            };

            return usermanager;
        }

        public static RoleManager<AppRole, int> RoleCreate()
        {
            var dbContext = new DataStore();
            var store = new RoleStore<AppRole, int, AppUserRole>(dbContext);
            var rolemanager = new RoleManager<AppRole, int>(store);
            return rolemanager;
        }
    }
}