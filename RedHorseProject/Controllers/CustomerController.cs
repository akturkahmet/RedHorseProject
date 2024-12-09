using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using RedHorseProject.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using AtvTour = EntityLayer.Concrete.Reservation;
using RedHorseProject.Models.ViewModel;
using EntityLayer.Concrete;

namespace RedHorseProject.Controllers
{

    [Authorize]
    public class CustomerController : Controller
    {

        private readonly IAgencyService _agencyService;
        private readonly IReservationService _ReservationService;
        RedHorseContext _context = new RedHorseContext();
        DataContext _datacontext = new DataContext();
        // _atvTourService.Insert(nesne) şeklinde dbye kaydediyor // 

        public CustomerController(IAgencyService agencyService, IReservationService ReservationService)
        {
            _agencyService = agencyService;
            _ReservationService = ReservationService;

        }

        public ActionResult Index()
        {
            using (RedHorseContext c = new RedHorseContext())
            {
                // Giriş yapan kullanıcının AgencyId'sini Session'dan al
                var agencyId = (int)Session["AgencyId"];

                // Bu Agency'ye bağlı araçları listele
                var atv = c.Reservations.Where(car => car.Agency_Id == agencyId).ToList();

                return View();
            }
        }

        public ActionResult CustomerRezervation()
        {
            var Reservation = _context.Reservations.ToList();
            return View(Reservation);
        }
        public JsonResult GetRezervation()
        {

            var reservations = _context.Reservations.ToList();
            return Json(reservations, JsonRequestBehavior.AllowGet);
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
        public ActionResult frmCreateReservation()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ChangePassword(string email, string newPassword, string confirmPassword)
        {


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
        public ActionResult frmEditRezervation(int id)
        {
            var reservation = _ReservationService.Get(x => x.Id == id);
            if (reservation == null)
            {
                return HttpNotFound("Reservation not found");
            }

            var viewModel = new EditReservationViewModel
            {
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                Phone = reservation.Phone,
                HotelName = reservation.HotelName,
                PassportNo = reservation.PassportNo,
                CustomerCount = reservation.CustomerCount,
                HotelRoomNo = reservation.HotelRoomNo
            };

            return PartialView("_EditReservationPartial", viewModel); // Partial view kullanıyoruz
        }


        public ActionResult EditRezervation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult updateStatus(int id, bool status)
        {

            var reservation = _context.Reservations.SingleOrDefault(r => r.Id == id);

            if (reservation.Status == true)
            {
                reservation.Status = false;
                _context.SaveChanges();
                return Json(new { success = true, message = "Rezervasyon pasif oldu." });
            }
            else
            {
                reservation.Status = true;
                _context.SaveChanges();
                return Json(new { success = true, message = "Rezervasyon aktif oldu." });
            }

        }
    }
}
