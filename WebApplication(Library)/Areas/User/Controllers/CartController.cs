using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication_Library_.Filters;
using WebApplication_Library_.Infrastructure;
using WebApplication_Library_.Models;
using WebApplication_Library_.Services;

namespace WebApplication_Library_.Areas.User.Controllers
{
    [AuthorizationFilter(Role.User)]
    public class CartController : Controller
    {
        private readonly ShoppingCartService _shoppingCartService;
        private readonly BookService _bookService;
        public CartController()
        {
            _shoppingCartService = (ShoppingCartService)AppServiceProvider.ServiceProvider.GetService(typeof(ShoppingCartService));
            _bookService = (BookService)AppServiceProvider.ServiceProvider.GetService(typeof(BookService));
        }

        // GET: User/Cart
        public async Task<ActionResult> Index()
        {
            var userId = (Session["CurrentUser"] as Models.User).Id;
            var books = await _bookService.GetQuery().Where(x => x.UserId == userId).ToListAsync();
            return View(books);
        }

        public async Task<ActionResult> BorrowBook(int bookId)
        {
            var result = await _shoppingCartService.BorrowBookAsync((Session["CurrentUser"] as Models.User).Id, bookId);

            if (result.IsError)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new Exception(result.Message))
                };
            }

            return RedirectToRoute("User_default", new { controller = "Shop", action = "Index" });
        }
        
        public async Task<ActionResult> GiveBackBook(int bookId)
        {
            var result = await _shoppingCartService.GiveBackBookAsync((Session["CurrentUser"] as Models.User).Id, bookId);

            if (result.IsError)
            {
                return new ViewResult()
                {
                    ViewName = "~/Views/Shared/Error.cshtml",
                    ViewData = new ViewDataDictionary(new Exception(result.Message))
                };
            }

            return RedirectToRoute("User_default", new { controller = "Cart", action = "Index" });
        }
    }
}