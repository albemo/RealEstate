using Microsoft.EntityFrameworkCore;
using RealEstate.Infrasctructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Infrasctructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _dbContext;
        public DbSet<TEntity> DbSet { get; }

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.FindAsync<TEntity>(id);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return PerformInclusions(DbSet, includeProperties);
        }



        private static IQueryable<TEntity> PerformInclusions(IQueryable<TEntity> query, IEnumerable<Expression<Func<TEntity, object>>> includeProperties = null)
        {
            if (includeProperties == null)
            {
                return query;
            }

            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
