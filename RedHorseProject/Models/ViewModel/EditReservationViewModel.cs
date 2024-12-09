using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models.ViewModel
{
    public class EditReservationViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string HotelName { get; set; }
        public string PassportNo { get; set; }
        public int HotelRoomNo { get; set; }
        public int? CustomerCount { get; set; }


    }
}