using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public IEnumerable<T> FilterList(Expression<Func<T, bool>> filter)
        {
            return _repository.FilterList(filter);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _repository.Get(filter);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(T entity)
        {
            _repository.Add(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
    }
}
