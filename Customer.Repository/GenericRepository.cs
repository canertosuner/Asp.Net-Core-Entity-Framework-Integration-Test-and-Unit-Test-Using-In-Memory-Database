using System;
using System.Linq;
using System.Linq.Expressions;
using Customer.Domain;
using Microsoft.EntityFrameworkCore;

namespace Customer.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly CustomerDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        protected GenericRepository(CustomerDbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<T>();
        }

        public Guid Save(T entity)
        {
            entity.Id = Guid.NewGuid();
            _dbSet.Add(entity);

            _dbContext.SaveChanges();

            return entity.Id;
        }

        public T Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

            _dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> All()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }
    }
}
