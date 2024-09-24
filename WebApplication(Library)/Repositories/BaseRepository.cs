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
    public abstract class BaseRepository<T> : ICRUIDRepository<T> where T : class
    {
        private DbSet<T> entities;
        protected DbSet<T> Entities => this.entities ?? _dbContext.Set<T>();

        protected LibraryDBContext _dbContext;
        public BaseRepository(LibraryDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract Task<OperationDetailsResponseModel> CreateAsync(T entity);

        public abstract Task<OperationDetailsResponseModel> DeleteAsync(int id);

        public IQueryable<T> GetQuery() => Entities.AsQueryable();

        public abstract Task<OperationDetailsResponseModel> UpdateAsync(int id, T newEntity);
    }
}