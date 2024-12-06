using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Agency
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Tc { get; set; }
        public string TaxNo { get; set; }
        public string TursabNo { get; set; }
        public string Mail { get; set; }
        public string Region { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        
        public bool isDeleted { get; set; }
    }

}