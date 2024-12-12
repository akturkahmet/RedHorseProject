using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class ReservationRepository : GenericRepository<Reservation>, IReservationRepository
    {

        public ReservationRepository(RedHorseContext context) : base(context)
        {
        }

    }
}

