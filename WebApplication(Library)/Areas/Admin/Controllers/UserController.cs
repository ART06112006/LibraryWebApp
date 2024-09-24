using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Filters;
using WebApplication_Library_.Infrastructure;
using WebApplication_Library_.Services;

namespace WebApplication_Library_.Areas.Admin.Controllers
{
    [AuthorizationFilter(Models.Role.Admin)]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = (UserService)AppServiceProvider.ServiceProvider.GetService(typeof(UserService));
        }

        // GET: Admin/User
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetQuery().Where(x => x.Id == x.Id).Include(x => x.BorrowedBooks).ToListAsync();
            return View(users);
        }

        public async Task<ActionResult> RemoveUser(int id)
        {
            var result = await _userService.DeleteAsync(id);

            if (result.IsError)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new Exception(result.Message))
                };
            }

            return RedirectToAction("Index");
        }
    }
}