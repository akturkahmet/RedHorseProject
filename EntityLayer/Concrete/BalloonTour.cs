using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class BalloonTour
    {
        public int Id { get; set; }
        public List<CustomerInformation> CustomerInformation { get; set; }
    }
}
