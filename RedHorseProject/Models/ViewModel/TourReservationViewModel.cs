using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models.ViewModel
{
    public class TourReservationViewModel
    {
        public List<AtvTour> AtvTours { get; set; }
        public List<BalloonTour> BalloonTours { get; set; }
        public List<CamelTour> CamelTours { get; set; }
        public List<HorseTour> HorseTours { get; set; }
        public List<JeepTour> JeepTours { get; set; }
    }
}