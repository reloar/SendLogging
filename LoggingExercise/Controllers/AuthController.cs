using LoggingExercise.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace LoggingExercise.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        private UserManager<AppUser, int> _userManager;
        public AuthController()
        {
            _userManager = Startup.UserManagerFactory.Invoke();
        }
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginInfo();
                return View(model);
          
        }

        [HttpPost]
        public ActionResult Login( LoginInfo login)
        {
            if (this.ModelState.IsValid)
            {
                var contact = _userManager.Find(login.Username, login.Password);
               
                if (contact != null)
                {

                    SignIn(login);

                    return Redirect(GetRedirectUrl(login.ReturnUrl));
                }
                else
                {
                    this.ModelState.AddModelError("", "Invalid Username or Password");
                }
            }
            return View();
           
        }

        private void SignIn(LoginInfo login)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("ApplicationCookie");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, login.Username));

            var ctxt = this.Request.GetOwinContext();
            ctxt.Authentication.SignIn(claimsIdentity);
        }
        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }
            return returnUrl;
        }
    }
}