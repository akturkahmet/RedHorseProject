﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TourType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

    }
}