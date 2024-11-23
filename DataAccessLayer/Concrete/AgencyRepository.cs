using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class AgencyRepository : GenericRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(RedHorseContext context) : base(context)
        {
        }
    }
}
