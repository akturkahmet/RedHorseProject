using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class SpecificDateCapacity
    {
        public int Id { get; set; }
        public string TourTypeId { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
        public int Capacity { get; set; }
        public bool Status { get; set; }
    }
}
