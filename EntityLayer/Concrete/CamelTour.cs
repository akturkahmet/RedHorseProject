using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CamelTour : Tour
    {
        public int CamelCount { get; set; }



        public int? AgenciesId { get; set; }

        // Agencies tablosu ile ilişkiyi kuruyoruz
        public virtual Agency Agencies { get; set; }

        // ForeignKey özniteliğini AgenciesId üzerinde kullanıyoruz
        [ForeignKey("AgenciesId")]
        public virtual Agency Agency { get; set; }

    }
}
