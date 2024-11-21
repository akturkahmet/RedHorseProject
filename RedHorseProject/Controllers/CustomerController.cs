using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedHorseProject.Controllers
{
    [Authorize(Roles = "Agency")]
    public class CustomerController : Controller
    {
        //GET: Customer
        public ActionResult Index()
        {
            using (Context c = new Context())
            {
                // Giriş yapan kullanıcının AgencyId'sini Session'dan al
                var agencyId = (int)Session["AgencyId"];

                // Bu Agency'ye bağlı araçları listele
                var atv = c.AtvTours.Where(car => car.AgenciesId == agencyId).ToList();

                return View(atv);
            }
        }
        public ActionResult AtvTurlari()
        {
            return View();
        }
        public ActionResult AtTurlari()
        {
            return View();
        }
        public ActionResult BalonTurlari()
        {
            return View();
        }
        public ActionResult DeveTurlari()
        {
            return View();
        }
        public ActionResult JeepSafariTurlari()
        {
            return View();
        }
        public ActionResult CustomerRezervation()
        {
            return View();
        }
        public ActionResult _Calendar()
        {
            // Gerekli veriyi burada oluşturabilirsiniz
            return PartialView("_Calendar");
        }
        public ActionResult CustomerSettings()
        {
            return View();
        }

    }
}