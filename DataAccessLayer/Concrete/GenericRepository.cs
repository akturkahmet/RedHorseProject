using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RedHorseContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(RedHorseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T t)
        {
            _dbSet.Add(t);
            _context.SaveChanges();
        }
        public void Delete(T t)
        {
            _dbSet.Remove(t);
            _context.SaveChanges();
        }

        public List<T> FilterList(Expression<Func<T, bool>> filter)
        {
            return _dbSet.AsNoTracking()
                .Where(filter)
                .ToList();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.AsNoTracking()
                .FirstOrDefault(filter);
        }

        public List<T> GetAll()
        {
            return _dbSet.AsNoTracking()
                .ToList();
        }

        public void Update(T t)
        {
            _context.Entry(t).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
