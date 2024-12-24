using EntityLayer.Concrete;
using System;
using System.Data.Entity;


namespace DataAccessLayer.Context
{
    public class RedHorseContext : DbContext
    {
        public DbSet<Agency> Agencys { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TourType> TourTypes { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<HoursCapacity> HoursCapacitys { get; set; }
        public DbSet<SpecificDateCapacity> SpecificDateCapacitys { get; set; }
    }
}
