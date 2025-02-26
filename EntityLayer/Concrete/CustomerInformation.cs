﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CustomerInformation
    {
        public int Id { get; set; }
       
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string CountryCode { get; set; }
        public int Phone { get; set; }
        public string HotelName { get; set; }
        public int HotelRoomNo { get; set; }
        public string PassportNo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
