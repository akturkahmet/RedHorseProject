using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{



    public class Agency
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Phone { get; set; }
        public int Tc { get; set; }
        public int TaxNo { get; set; }
        public int TursabNo { get; set; }
        public string Mail { get; set; }
        public string Region { get; set; }
        public DateTime CreatedDate { get; set; }

    }

}