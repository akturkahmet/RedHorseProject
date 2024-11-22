using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        List<T> GetAll();
        List<T> FilterList(Expression<Func<T, bool>> filter);
        T Get(Expression<Func<T, bool>> filter);
        void Delete(T t);

    }
}
