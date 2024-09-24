using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Models;
using WebApplication_Library_.Repositories;

namespace WebApplication_Library_.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationDetailsResponseModel> CreateAsync(User user) => await _userRepository.CreateAsync(user);
        public async Task<OperationDetailsResponseModel> DeleteAsync(int id) => await _userRepository.DeleteAsync(id);
        public async Task<OperationDetailsResponseModel> UpdateAsync(int id, User newUser) => await _userRepository.UpdateAsync(id, newUser);
        public IQueryable<User> GetQuery() => _userRepository.GetQuery();
    }
}