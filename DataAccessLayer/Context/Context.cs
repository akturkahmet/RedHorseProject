using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class Context : DbContext
    {
        public DbSet<Agency> Agencys { get; set; }
        public DbSet<AtvTour> AtvTours { get; set; }
        public DbSet<BalloonTour> BalloonTours { get; set; }
        public DbSet<CamelTour> CamelTours { get; set; }
        public DbSet<CustomerInformation> CustomerInformations { get; set; }
        public DbSet<HorseTour> HorseTours { get; set; }
        public DbSet<JeepTour> JeepTours { get; set; }
        public DbSet<Admin> Admins { get; set; }

    }
}
