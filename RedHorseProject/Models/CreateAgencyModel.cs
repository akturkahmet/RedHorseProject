using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models
{
    public class CreateAgencyModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string TursabNo { get; set; }
        public string TcNo { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
    }
}