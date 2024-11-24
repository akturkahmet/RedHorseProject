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
    public class AtvTourManager : GenericManager<AtvTour>, IAtvTourService
    {
        public AtvTourManager(IGenericRepository<AtvTour> repository) : base(repository)
        {
        }
    }
}
