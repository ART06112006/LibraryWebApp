using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Repositories.IRepositories
{
    public interface IDeleteRangeRepository<T> where T : class
    {
        Task<OperationDetailsResponseModel> DeleteRangeAsync(List<T> range);
    }
}