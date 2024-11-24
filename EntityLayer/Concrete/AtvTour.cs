using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AtvTour
    {
        public int Id { get; set; }
        public int AtvCount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string HotelName { get; set; }
        public int HotelRoomNo { get; set; }
        public string PassportNo { get; set; }
        public int CustomerCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public TimeSpan ReservationTime { get; set; } 
        public string TourNote { get; set; }
        public bool Status { get; set; }
        public virtual Agency Agencies { get; set; }
        [ForeignKey(nameof(Agencies))]
        public int AgenciesId { get; set; }

    }
}
