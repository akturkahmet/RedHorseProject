﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class AtvTour : Tour
    {
        public int? Capacity { get; set; }
        public Nullable<DateTime> ReservationDate { get; set; }

        // Foreign Key özelliğini AgenciesId olarak ekliyoruz
        public int? AgenciesId { get; set; }

        // Agencies tablosu ile ilişkiyi kuruyoruz
        public virtual Agency Agencies { get; set; }

        // ForeignKey özniteliğini AgenciesId üzerinde kullanıyoruz
        [ForeignKey("AgenciesId")]
        public virtual Agency Agency { get; set; }
    }
}
