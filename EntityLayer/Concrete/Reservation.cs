using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Reservation
    {
        public int Id { get; set; }
        public string TourType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string HotelName { get; set; }
        public int? HotelRoomNo { get; set; }
        public string PassportNo { get; set; }
        public int CustomerCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public string TourNote { get; set; }
        public int? Agency_Id { get; set; }
        public bool Status { get; set; }


    }
}
