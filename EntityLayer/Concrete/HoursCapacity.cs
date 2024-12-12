using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class HoursCapacity
    {

        public int Id { get; set; }
        public string TourTypeId { get; set; }
        public string Hour { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }

    }
}
