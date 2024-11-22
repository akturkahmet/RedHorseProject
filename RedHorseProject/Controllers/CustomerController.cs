using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
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
        private readonly IAgencyService _agencyService;

        public CustomerController(IAgencyService agencyService)
        {
            _agencyService = agencyService;
        }

        public ActionResult Index()
        {
            using (RedHorseContext c = new RedHorseContext())
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

            return PartialView("_Calendar");
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(string email, string newPassword, string confirmPassword)
        {
            //Eraya tten//

            var currentEmail = Session["Mail"]?.ToString();  // Giriş yapan kullanıcının e-mail adresini çektük //
            var agency = _agencyService.Get(a => a.Mail == currentEmail);

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Lütfen Boş Bırakmayınız." });
            }

            if (email != currentEmail)
            {
                return Json(new { success = false, message = "Hatalı E-Mail Adresi." });
            }

            if (newPassword != confirmPassword)
            {
                return Json(new { success = false, message = "Yeni şifre ile şifre onayı eşleşmiyor." });
            }

            agency.Password = newPassword;

            _agencyService.Update(agency);

            return Json(new { success = true, message = "Şifreniz başarıyla değiştirildi." });
        }

    }
}