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

namespace WebApplication_Library_.Areas.User.Controllers
{
    [AuthorizationFilter(Models.Role.User)]
    public class ShopController : Controller
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public ShopController()
        {
            _bookService = (BookService)AppServiceProvider.ServiceProvider.GetService(typeof(BookService));
            _userService = (UserService)AppServiceProvider.ServiceProvider.GetService(typeof(UserService));
        }

        
        // GET: User/Shop
        public async Task<ActionResult> Index()
        {
            var books = await _bookService.GetQuery().Where(x => x.Id == x.Id).ToListAsync();

            var booksCount = books.Count(x => x.UserId == (Session["CurrentUser"] as Models.User).Id);
            if (booksCount == 5)
            {
                ViewBag.ShowLimit = true;
            }
            else
            {
                ViewBag.ShowLimit = false;
            }
            ViewBag.ShoppingCartBooksCount = booksCount;

            var userId = (Session["CurrentUser"] as Models.User).Id;
            ViewBag.CurrentUser = await _userService.GetQuery().FirstOrDefaultAsync(x => x.Id == userId);

            return View(books);
        }

        public async Task<ActionResult> UpdateUser(int id, string name, string email, DateTime dateOfBirth)
        {
            var user = await _userService.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            user.Name = name;
            user.Email = email;
            user.DateOfBirth = dateOfBirth;

            var result = await _userService.UpdateAsync(id, user);

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