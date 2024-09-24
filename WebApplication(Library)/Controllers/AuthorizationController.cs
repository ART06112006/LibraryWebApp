using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication_Library_.Infrastructure;
using WebApplication_Library_.Models;
using WebApplication_Library_.Services;

namespace WebApplication_Library_.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly AuthorizationService _authorizationService;
        private readonly UserService _userService;
        public AuthorizationController()
        {
            _authorizationService = (AuthorizationService)AppServiceProvider.ServiceProvider.GetService(typeof(AuthorizationService));
            _userService = (UserService)AppServiceProvider.ServiceProvider.GetService(typeof(UserService));
        }

        // GET: Authorization
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            var result = await _authorizationService.RegisterUserAsync(user);

            if (result.IsError)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new Exception(result.Message))
                };
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(string login, string password)
        {
            var user = await _authorizationService.AuthentificateUserAsync(login, password);

            if (user == null)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new Exception("User is not registered"))
                };
            }

            Session["CurrentUser"] = user;

            FormsAuthentication.SetAuthCookie(user.Login, false);

            switch (user.Role)
            {
                case Role.Admin:
                    return RedirectToRoute("Admin_default", new { controller = "Home", action = "Index" });
                case Role.User:
                    return RedirectToRoute("User_default", new { controller = "Shop", action = "Index" });
                default:
                    return new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary(new Exception("Not identified user role"))
                    };

            }
        }
    }
}