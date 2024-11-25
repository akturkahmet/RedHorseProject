using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using RedHorseProject.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AtvTour = EntityLayer.Concrete.AtvTour;
using RedHorseProject.Models.ViewModel;

namespace RedHorseProject.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly IAgencyService _agencyService;
        private readonly IAtvTourService _atvTourService;
        RedHorseContext _context = new RedHorseContext();
        DataContext _datacontext = new DataContext();
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
            var unapproved = GetUnapprovedAllRezervation(); 
            var approved = GetapprovedAllRezervation(); 

            var model = new AtvTourViewModel
            {
                UnapprovedReservations = unapproved,
                ApprovedReservations = approved
            };

            return View(model); // View'e model gönderiyoruz
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




        [HttpPost]
        public JsonResult SaveRezervation(string name, string surname, string phone, string hotelName, int roomNumber, string reservationTime,string passportNo,int? customerCount)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            try
            {
                // Gelen tarihi uygun formata dönüştürme
                DateTime selectedDateTime;
                if (DateTime.TryParse(reservationTime, out selectedDateTime))
                {
                    // Eğer tarih geçerli bir DateTime ise kayda devam et
                    var newRezervation = new EntityLayer.Concrete.AtvTour
                    {
                        FirstName = name,
                        LastName = surname,
                        Phone = phone,
                        HotelName = hotelName,
                        HotelRoomNo = roomNumber,
                        CreatedDate = DateTime.Now,
                        ReservationDate = selectedDateTime,
                        PassportNo = passportNo,
                        CustomerCount = customerCount,
                        Agency_Id = userId
                    };

                    _atvTourService.Insert(newRezervation);

                    return Json(new { success = true, message = "Rezervasyon başarıyla kaydedildi." });
                }
                else
                {
                    // Geçersiz tarih formatı
                    return Json(new { success = false, message = "Geçersiz tarih formatı." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Hata oluştu: " + ex.Message });
            }
        }

        public List<AtvTour> GetUnapprovedAllRezervation()
        {
            var unaproved = _context.AtvTours
                                      .Where(t => t.Status == false)
                                      .ToList();
            return unaproved;
        }
        [HttpGet]
        public List<AtvTour> GetapprovedAllRezervation()
        {
            var aproved = _context.AtvTours
                                      .Where(t => t.Status == true)
                                      .ToList();
            return aproved;
        }
        [HttpGet]
        public JsonResult GetRezervation(int id)
        {

            var query = _context.AtvTours
                                      .Where(t => t.Id == id)
                                      .FirstOrDefault();




                return Json(query, JsonRequestBehavior.AllowGet);
            


        }

    }
}