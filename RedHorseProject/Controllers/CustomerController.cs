using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using RedHorseProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedHorseProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IAgencyService _agencyService;
        private readonly IAtvTourService _atvTourService;
        RedHorseContext _context = new RedHorseContext();
        // _atvTourService.Insert(nesne) şeklinde dbye kaydediyor // 

        public CustomerController(IAgencyService agencyService, IAtvTourService atvTourService)
        {
            _agencyService = agencyService;
            _atvTourService = atvTourService;

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

            var list = GetUnapprovedAllRezervation();

            return View(list);
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
        public ActionResult Editİnformation()
        {
            return View();
        }
        public ActionResult frmDetails()
        {
            return View("frmDetails");
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


        ///ÖRNEK/// 

        //[HttpPost]
        //public JsonResult SubmitAtvTourReservation(ReservationViewModel model)
        //{
        //    int? agencyId = Session["AgencyId"] as int?;

        //    if (ModelState.IsValid)
        //    {
        //        var reservation = new EntityLayer.Concrete.AtvTour
        //        {
        //            FirstName = model.FirstName,
        //            Phone = model.Phone,
        //            HotelName = model.HotelName,
        //            HotelRoomNo = model.HotelRoomNo,
        //            CreatedDate = DateTime.Now,
        //            ReservationTime = model.ReservationTime,
        //            AgenciesId = agencyId.Value,
        //            CustomerCount = model.CustomerCount,
        //        };

        //        _atvTourService.Insert(reservation);
        //        return Json(new { success = true, message = "Rezervasyon başarıyla kaydedildi!" });
        //    }
        //    return Json(new { success = false, message = "Geçerli bir veri gönderilmedi." });
        //}


        [HttpPost]
        public JsonResult SaveRezervation(string name, string surname, string phone, string hotelName, int roomNumber, string reservationTime)
        {
            try
            {
                DateTime selectedDateTime = DateTime.ParseExact(reservationTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                // ReservationDate'e seçilen tarihi atıyoruz
                var newRezervation = new EntityLayer.Concrete.AtvTour
                {
                    FirstName = name,
                    LastName = surname,
                    Phone = phone,
                    HotelName = hotelName,
                    HotelRoomNo = roomNumber,
                    ReservationDate = selectedDateTime // sadece gün, ay, yıl kullanılıyor
                };

                // Veritabanına ekle
                _atvTourService.Insert(newRezervation);

                return Json(new { success = true, message = "Rezervasyon başarıyla kaydedildi." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata oluştu: " + ex.Message });
            }
        }
        public List<AtvTour> GetUnapprovedAllRezervation()
        {
            var rezervation = _context.AtvTours
                                      .Where(t => t.Status == false)
                                      .ToList();
            return rezervation;
        }
        [HttpGet]
        public JsonResult GetapprovedAllRezervation()
        {
            var approvedrezervation = _context.AtvTours
                                      .Where(t => t.Status == true)
                                      .ToList();

            return Json(approvedrezervation, JsonRequestBehavior.AllowGet);
        }
    }
}