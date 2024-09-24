using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Models;
using WebApplication_Library_.Repositories;

namespace WebApplication_Library_.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;
        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<OperationDetailsResponseModel> CreateAsync(Book book) => await _bookRepository.CreateAsync(book);
        public async Task<OperationDetailsResponseModel> DeleteAsync(int id)
        {
            try
            {
                var book = await GetQuery().FirstOrDefaultAsync(x => x.Id == id);
                if (book.IsBorrowed)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Book is borrowed" };
                }

                return await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetailsResponseModel> DeleteAllAsync()
        {
            try
            {
                var books = await GetQuery().ToListAsync();
                return await _bookRepository.DeleteRangeAsync(books);
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetailsResponseModel> UpdateAsync(int id, Book newBook) => await _bookRepository.UpdateAsync(id, newBook);
        public IQueryable<Book> GetQuery() => _bookRepository.GetQuery();
    }
}