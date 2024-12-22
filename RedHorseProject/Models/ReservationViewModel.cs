using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models
{
    public class ReservationViewModel
    {

  
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Hour { get; set; }
            public string Phone { get; set; }
            public string HotelName { get; set; }
            public int HotelRoomNo { get; set; }
            public string PassportNo { get; set; }
            public int CustomerCount { get; set; }
            public DateTime ReservationDate { get; set; }
            public bool Status { get; set; }
            public string TourNote { get; set; }
            public string AgencyName { get; set; }
            public string TourTypeName { get; set; }
        
    }
}