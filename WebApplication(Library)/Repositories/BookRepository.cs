using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Context;
using WebApplication_Library_.Models;
using WebApplication_Library_.Repositories.IRepositories;

namespace WebApplication_Library_.Repositories
{
    public class BookRepository : BaseRepository<Book>, IDeleteRangeRepository<Book>
    {
        public BookRepository(LibraryDBContext dbContext) : base(dbContext) { }

        public override async Task<OperationDetailsResponseModel> CreateAsync(Book book)
        {
            try
            {
                _dbContext.Books.Add(book);
                await _dbContext.SaveChangesAsync();
                return new OperationDetailsResponseModel { IsError = false };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public override async Task<OperationDetailsResponseModel> DeleteAsync(int id)
        {
            try
            {
                var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Book was not found" };
                }

                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();

                return new OperationDetailsResponseModel { IsError = false };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public async Task<OperationDetailsResponseModel> DeleteRangeAsync(List<Book> books)
        {
            try
            {
                _dbContext.Books.RemoveRange(books);
                await _dbContext.SaveChangesAsync();

                return new OperationDetailsResponseModel { IsError = false };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public override async Task<OperationDetailsResponseModel> UpdateAsync(int id, Book newBook)
        {
            try
            {
                var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "Book was not found" };
                }

                book.Title = newBook.Title;
                book.Author = newBook.Author;
                book.YearPublished = newBook.YearPublished;
                book.Photo = newBook.Photo;
                book.UserId = newBook.UserId;
                book.IsBorrowed = newBook.IsBorrowed;

                _dbContext.Entry(book).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                return new OperationDetailsResponseModel { IsError = false };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }
    }
}