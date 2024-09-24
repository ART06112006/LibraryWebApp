using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Context;
using WebApplication_Library_.Models;
using WebApplication_Library_.Repositories.IRepositories;
using WebApplication_Library_.Services;

namespace WebApplication_Library_.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(LibraryDBContext dbContext) : base(dbContext) { }

        public override async Task<OperationDetailsResponseModel> CreateAsync(User user)
        {
            try
            {
                user.Password = PasswordHasher.HashPassword(user.Password);
                _dbContext.Users.Add(user);
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
                var user = await _dbContext.Users.Where(x => x.Id == id).Include(x => x.BorrowedBooks).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "User was not found" };
                }

                user.BorrowedBooks = null;
                await UpdateAsync(user.Id, user);

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return new OperationDetailsResponseModel { IsError = false };
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public override async Task<OperationDetailsResponseModel> UpdateAsync(int id, User newUser)
        {
            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "User was not found" };
                }

                user.Name = newUser.Name;
                user.Email = newUser.Email;
                user.DateOfBirth = newUser.DateOfBirth;

                _dbContext.Entry(user).State = EntityState.Modified;
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