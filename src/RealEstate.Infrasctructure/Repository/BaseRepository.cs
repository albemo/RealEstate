using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrasctructure.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public IRepository<TEntity> _repository { get; }

        //public DbSet<TEntity> dbSet { get; set; }

        public BaseRepository(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            await _repository.DeleteAsync(entity);

            return true;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            return await _repository.UpdateAsync(entity);
        }


        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return _repository.Query(includeProperties);
        }
    }
}
