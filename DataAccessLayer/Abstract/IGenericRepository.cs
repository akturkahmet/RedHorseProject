using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T t);
        void Update(T t);
        List<T> GetAll();
        T GetById(int id);
        void Delete(int id);
    }
}
