using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Repositories.IRepositories
{
    public interface ICRUIDRepository<T> where T : class
    {
        Task<OperationDetailsResponseModel> CreateAsync(T entity);
        IQueryable<T> GetQuery();
        Task<OperationDetailsResponseModel> UpdateAsync(int id, T newEntity);
        Task<OperationDetailsResponseModel> DeleteAsync(int id);
    }
}