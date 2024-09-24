using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;
using System.Xml.Linq;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Services
{
    public class AuthorizationService
    {
        private readonly UserService _userService;

        public AuthorizationService(UserService userService)
        {
            _userService = userService;
        }

        public async Task<OperationDetailsResponseModel> RegisterUserAsync(User user)
        {
            try
            {
                var userExists = await _userService.GetQuery().FirstOrDefaultAsync(x => x.Login == user.Login);
                if (userExists != null)
                {
                    return new OperationDetailsResponseModel { IsError = true, Message = "User with such a login already exists"};
                }

                return await _userService.CreateAsync(user);
            }
            catch (Exception ex)
            {
                return new OperationDetailsResponseModel { IsError = true, Message = ex.Message };
            }
        }

        public async Task<User> AuthentificateUserAsync(string login, string password)
        {
            
            var user = await _userService.GetQuery().FirstOrDefaultAsync(x => x.Login == login);

            if (user != null && PasswordHasher.VerifyPassword(user.Password, password))
            {
                return user;
            }

            return null;
        }
    }
}