using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedHorseProject.Models
{
    public class ReservationViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string HotelName { get; set; }
        public string PassportNo { get; set; }
        public int CustomerCount { get; set; }
        public int HotelRoomNo { get; set; }
        [Required]
        public TimeSpan ReservationTime { get; set; }
        public int AgenciesId { get; set; }
    }
}