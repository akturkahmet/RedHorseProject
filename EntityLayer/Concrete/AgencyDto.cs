using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models.ViewModel
{
    public class AgencyDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgencyName { get; set; }
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
        public bool isApproved { get; set; }
    }
}