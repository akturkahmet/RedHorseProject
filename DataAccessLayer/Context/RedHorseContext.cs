using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class RedHorseContext : DbContext
    {
        public DbSet<Agency> Agencys { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<HoursCapacity> HoursCapacitys { get; set; }

    }
}
