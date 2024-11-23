using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class AdminManager : GenericManager<Admin>, IAdminService
    {
        public AdminManager(IGenericRepository<Admin> repository) : base(repository)
        {
        }
    }
}
