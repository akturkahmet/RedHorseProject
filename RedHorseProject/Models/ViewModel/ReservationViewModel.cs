using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models.ViewModel
{
    public class ReservationViewModel
    {
        public List<Reservation> Tours { get; set; }
       
    }
}