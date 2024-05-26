using Payment.Service.Domain.Abstractions;
using Payment.Service.Infrastructure.Persistense.Context;
using Microsoft.EntityFrameworkCore;

namespace Payment.Service.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected readonly PaymentDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(PaymentDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}