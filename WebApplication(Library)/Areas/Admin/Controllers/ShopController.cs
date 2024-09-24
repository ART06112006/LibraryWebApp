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

namespace WebApplication_Library_.Areas.Admin.Controllers
{
    [AuthorizationFilter(Role.Admin)]
    public class ShopController : Controller
    {
        private readonly BookService _bookService;

        public ShopController()
        {
            _bookService = (BookService)AppServiceProvider.ServiceProvider.GetService(typeof(BookService));
        }

        // GET: Admin/Shop
        public async Task<ActionResult> Index()
        {
            var books = await _bookService.GetQuery().Where(x => x.Id == x.Id).ToListAsync();
            return View(books);
        }

        public async Task<ActionResult> AddBook(Book book)
        {
            var result = await _bookService.CreateAsync(book);

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

        public async Task<ActionResult> RemoveBook(int id)
        {
            var result = await _bookService.DeleteAsync(id);

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

        public async Task<ActionResult> RemoveAllBooks()
        {
            var result = await _bookService.DeleteAllAsync();

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

        public async Task<ActionResult> UpdateBook(int id, string title, string author, int yearPublished, string photo)
        {
            var book = await _bookService.GetQuery().FirstOrDefaultAsync(x => x.Id == id);
            book.Title = title;
            book.Author = author;
            book.YearPublished = yearPublished;
            book.Photo = photo;

            var result = await _bookService.UpdateAsync(id, book);

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