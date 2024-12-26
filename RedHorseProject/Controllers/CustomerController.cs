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
using System.Security.Cryptography;
using System.Text;

namespace RedHorseProject.Controllers
{

    [Authorize]
    public class CustomerController : Controller
    {

        private readonly IAgencyService _agencyService;
        private readonly IReservationService _ReservationService;
        RedHorseContext _context = new RedHorseContext();
        DataContext _datacontext = new DataContext();

        public CustomerController(IAgencyService agencyService, IReservationService ReservationService)
        {
            _agencyService = agencyService;
            _ReservationService = ReservationService;

        }

        public ActionResult Index()
        {

            using (RedHorseContext c = new RedHorseContext())
            {
                return View();
            }
        }

        public ActionResult CustomerRezervation()
        {
            var Reservation = _context.Reservations.ToList();
            return View(Reservation);
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult EditInformation()
        {
            var agencyId = Session["AgencyId"] as int?;
            var agencyInformation = _context.Agencys.Where(a => a.Id == agencyId).FirstOrDefault();
            return View(agencyInformation);
        }
        public ActionResult frmDetails()
        {
            return View();
        }
        public ActionResult frmCreateReservation()
        {
            return View();
        }
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
        [HttpPost]
        public JsonResult ChangePassword(string email, string newPassword, string confirmPassword)
        {


            var currentEmail = Session["Mail"]?.ToString();
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

            agency.Password = HashPassword(newPassword);

            _agencyService.Update(agency);

            return Json(new { success = true, message = "Şifreniz başarıyla değiştirildi." });
        }
        public ActionResult frmEditRezervation()
        {
            return PartialView("_EditReservationPartial");
        }


        public ActionResult EditRezervation()
        {
            return View();
        }
        public ActionResult ReservationCreater()
        {
            return View();
        }

        public JsonResult GetRezervation()
        {


            int agencyId = (int)Session["AgencyId"];

            var reservations = _context.Reservations
                 .Where(r => r.Agency_Id == agencyId)
                 .Join(
                     _context.TourTypes,          
                     r => r.TourType,               
                     t => t.Id,                      
                     (r, t) => new                
                     {
                         r.Id,
                         r.TourType,
                         r.FirstName,
                         r.LastName,
                         r.Mail,
                         r.CountryCode,
                         r.Phone,
                         r.HotelName,
                         r.HotelRoomNo,
                         r.PassportNo,
                         r.CustomerCount,
                         r.CreatedDate,
                         r.ReservationDate,
                         r.TourNote,
                         r.Agency_Id,
                         r.Status,
                         t.Name,          
                         IsFutureReservation = r.ReservationDate >= DateTime.Now
                     }
                 )
                 .ToList();


            return new CustomJsonResult
            {
                Data = reservations
            };

        }





        [HttpPost]
        public ActionResult updateStatus(int id)
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

        [HttpGet]
        public JsonResult GetReservationDetails(int id)
        {

            var reservation = _context.Reservations.Where(r => r.Id == id).FirstOrDefault();
            return Json(reservation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateReservation(int ReservationId, string FirstName, string LastName, string Phone, string HotelName, string PassportNo, int CustomerCount, int? HotelRoomNo)
        {
            try
            {
                var reservation = _context.Reservations.FirstOrDefault(r => r.Id == ReservationId);

                if (reservation == null)
                {
                    return Json("Reservation not found");
                }

                reservation.FirstName = FirstName;
                reservation.LastName = LastName;
                reservation.Phone = Phone;
                reservation.HotelName = HotelName;
                reservation.PassportNo = PassportNo;
                reservation.CustomerCount = CustomerCount;
                reservation.HotelRoomNo = HotelRoomNo;

                _context.SaveChanges();

                return Json(new { Message = "Reservation updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error updating reservation", Error = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult UpdateAgencyInformation(int AgencyId, string AgencyName, string Phone)
        {
            try
            {
                var agency = _context.Agencys.FirstOrDefault(r => r.Id == AgencyId);

                if (agency == null)
                {
                    return Json("Agency not found");
                }

                agency.Phone = Phone;
                agency.AgencyName = AgencyName;
                _context.SaveChanges();

                return Json(new { Message = "Reservation updated successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Error updating reservation", Error = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult ChangeCustomerPassword(string Email, string Password, string ConfirmPassword)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
            {
                return Json(new { success = false, message = "Tüm alanlar doldurulmalıdır." });
            }

            if (Password != ConfirmPassword)
            {
                return Json(new { success = false, message = "Şifreler eşleşmiyor." });
            }

            var user = _context.Agencys.FirstOrDefault(u => u.Mail == Email);

            if (user == null)
            {
                return Json(new { success = false, message = "Email adresi bulunamadı." });
            }

            user.Password = HashPassword(Password);
            _context.SaveChanges();

            return Json(new { success = true, message = "Şifreniz başarıyla güncellendi." });
        }


        [HttpPost]
        public JsonResult CreateReservation(Reservation model)
        {
            int agencyId = (int)Session["AgencyId"];
            _context.Reservations.Add(new Reservation
            {
                TourType = model.TourType,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Mail = model.Mail,
                CountryCode = "TR",
                Phone = model.Phone,
                HotelName = model.HotelName,
                HotelRoomNo = model.HotelRoomNo,
                PassportNo = model.PassportNo,
                CustomerCount = model.CustomerCount,
                CreatedDate = DateTime.Now,
                Agency_Id = agencyId,
                ReservationDate = model.ReservationDate,
                TourNote = model.TourNote,
                Status = true,
            });
            _context.SaveChanges();

            return Json(new { success = true, message = "Reservation completed successfully." });


        }


        public JsonResult ControlReservationDate(string TourTypeId, string Hour, int CustomerCount, DateTime ReservationDate)
        {
            var formattedReservationDate = ReservationDate.ToString("yyyy-MM-dd");
            var isExistRecord = _context.SpecificDateCapacitys
                                        .Any(x => x.TourTypeId == TourTypeId && x.Day == formattedReservationDate && x.Hour == Hour);

            if (isExistRecord)
            {
                var specificRecord = _context.SpecificDateCapacitys
                                              .Where(x => x.TourTypeId == TourTypeId && x.Day == formattedReservationDate && x.Hour == Hour)
                                              .FirstOrDefault();

                var reservationCustomerCount = _context.Reservations.Where(x => x.ReservationDate == ReservationDate && x.TourType == TourTypeId && x.Status == true).ToList().Sum(x => x.CustomerCount);


                if (specificRecord == null)
                {
                    return Json(new { success = true });
                }
                int remainingCapacity = specificRecord.Capacity - reservationCustomerCount ?? 0;
                if (remainingCapacity >= CustomerCount)

                {
                    return Json(new { success = true, remainingCapacity });
                }
                else
                {
                    if (remainingCapacity <= 0)
                    {
                        return Json(new { success = false, message = $"Yer kalmadı", remainingCapacity });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"Sadece {remainingCapacity} kişilik yer kaldı.", remainingCapacity });

                    }

                }
            }
            else
            {
                var capacity = _context.HoursCapacitys.Where(x => x.TourTypeId == TourTypeId && x.Hour == Hour).FirstOrDefault();
                var reservationCustomerCount = _context.Reservations.Where(x => x.ReservationDate == ReservationDate && x.TourType == TourTypeId && x.Status == true).ToList().Sum(x => x.CustomerCount);
                if (capacity == null)
                {
                    return Json(new { success = true });
                }
                int remainingCapacity = capacity.Capacity - reservationCustomerCount ?? 0;
                if (remainingCapacity >= CustomerCount)

                {
                    return Json(new { success = true, remainingCapacity });
                }
                else
                {
                    if (remainingCapacity <= 0)
                    {
                        return Json(new { success = false, message = $"Yer kalmadı", remainingCapacity });
                    }
                    else
                    {
                        return Json(new { success = false, message = $"Sadece {remainingCapacity} kişilik yer kaldı.", remainingCapacity });

                    }

                }
            }

        }
        [HttpPost]
        public JsonResult ControlHours(string TourTypeId, string Date1)
        {

            var defaultHours = _context.HoursCapacitys
                .Where(x => x.TourTypeId == TourTypeId)
                .ToList();

            var specificHours = _context.SpecificDateCapacitys
                .Where(x => x.Day == Date1 && x.TourTypeId == TourTypeId)
                .ToList();

            List<object> combinedHours = new List<object>();

            for (int i = 0; i < 25; i++)
            {
                string formattedHour = i.ToString("D2") + ":00";
                var specificHourData = specificHours.FirstOrDefault(x => x.Hour == formattedHour);
                if (specificHourData != null)
                {
                    combinedHours.Add(specificHourData);
                }
                else
                {
                    var defaultHourData = defaultHours.FirstOrDefault(x => x.Hour == formattedHour);
                    if (defaultHourData != null)
                    {
                        combinedHours.Add(defaultHourData);
                    }
                }
            }

            return Json(combinedHours, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ControlHourCapacity(string TourTypeId, string Hour, DateTime ReservationDate)
        {
            var formattedReservationDate = ReservationDate.ToString("yyyy-MM-dd");
            var isExistRecord = _context.SpecificDateCapacitys
                                        .Any(x => x.TourTypeId == TourTypeId && x.Day == formattedReservationDate && x.Hour == Hour);

            if (isExistRecord)
            {
                var specificRecord = _context.SpecificDateCapacitys
                                              .Where(x => x.TourTypeId == TourTypeId && x.Day == formattedReservationDate && x.Hour == Hour)
                                              .FirstOrDefault();

                return Json(specificRecord.Capacity, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var capacity = _context.HoursCapacitys.Where(x => x.TourTypeId == TourTypeId && x.Hour == Hour).FirstOrDefault();
                var reservationCustomerCount = _context.Reservations.Where(x => x.ReservationDate == ReservationDate && x.TourType == TourTypeId).ToList().Sum(x => x.CustomerCount);
                int remainingCapacity = capacity.Capacity - reservationCustomerCount ?? 0;

                return Json(remainingCapacity, JsonRequestBehavior.AllowGet);

            }
        }

    }
}
