using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Services
{
    public class ShoppingCartService
    {
        private readonly BookService _bookService;
        private readonly UserService _userService;

        public ShoppingCartService(BookService bookService, UserService userService)
        {
            _bookService = bookService;
            _userService = userService;
        }

        public async Task<OperationDetailsResponseModel> BorrowBookAsync(int userId, int bookId)
        {
            try
            {
                var book = await _bookService.GetQuery().FirstOrDefaultAsync(x => x.Id == bookId);
                if (book == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Book was not found" };
                }

                var user = await _userService.GetQuery().Where(x => x.Id == userId).Include(x => x.BorrowedBooks).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "User was not found" };
                }

                if (user.BorrowedBooks.Count >= 5)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Borrowing limit (5 books) is reached" };
                }

                book.UserId = user.Id;
                book.IsBorrowed = true;

                return await _bookService.UpdateAsync(book.Id, book);
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetailsResponseModel> GiveBackBookAsync(int userId, int bookId)
        {
            try
            {
                var book = await _bookService.GetQuery().FirstOrDefaultAsync(x => x.Id == bookId);
                if (book == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Book was not found" };
                }

                book.UserId = null;
                book.IsBorrowed = false;

                return await _bookService.UpdateAsync(book.Id, book);
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }
    }
}