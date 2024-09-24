using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Filters;

namespace WebApplication_Library_.Areas.Admin.Controllers
{
    [AuthorizationFilter(Models.Role.Admin)]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}