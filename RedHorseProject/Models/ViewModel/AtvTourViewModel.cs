using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedHorseProject.Models.ViewModel
{
    public class AtvTourViewModel
    {
        public List<AtvTour> UnapprovedReservations { get; set; }
        public List<AtvTour> ApprovedReservations { get; set; }
    }
}
