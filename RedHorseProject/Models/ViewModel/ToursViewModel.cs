using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models.ViewModel
{
    public class ToursViewModel
    {
        public List<Tour> ActiveTours { get; set; }
        public List<Tour> InactiveTours { get; set; }
    }

}