using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using RedHorseProject.Helper;
using RedHorseProject.Models;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace RedHorseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminService _service;
        private readonly IAgencyService _agencyService;
        RedHorseContext _context = new RedHorseContext();

        public HomeController(IAdminService service, IAgencyService agencyService)
        {
            _service = service;
            _agencyService = agencyService;
        }

        public ActionResult Index()
        {
            var list = _context.Agencys
            .Where(t => t.isDeleted == false && t.isApproved == true)
            .ToList();
            return View(list);
        }

        public ActionResult About()
        {
            return View();
        } 
        public ActionResult frmAgencyDetails()
        {
            return View();
        }
        public ActionResult SpecificClockSet()
        {
            return View();
        }
        public ActionResult SpecificEditCapacity()
        {
            return View();
        }

        public ActionResult EditClock()
        {
            return View();
        }
        public ActionResult EditCapacity()
        {
            return View();
        }

        public ActionResult Rezervation()
        {
            // Aktif turlar
            var activeAtvTours = _context.Reservations
                .Where(t => t.Status == true)
                .ToList();
            return View();

        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult CustomerCreator()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateCustomer(CreateAgencyModel model)
        {
            // Model doğrulama
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Geçersiz veri!" });
            }

            var newAgency = new Agency
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AgencyName = model.AgencyName,
                Mail = model.Mail,
                Phone = model.Phone,
                TursabNo = model.TursabNo,
                Tc = model.TcNo,
                Region = model.Region,
                Password = model.Password,
                TaxNo = "boş",
                Role = "Agency",
                Status = true,
                CreatedDate = DateTime.Now
            };

            _agencyService.Insert(newAgency);

            return Json(new { success = true, message = "Acenta Başarıyla Eklendi!" });
        }

        public ActionResult Appeals()
        {
            return View();
        }
        public ActionResult _CustomerDetails()
        {

            return PartialView("_CustomerDetails");
        }
        public ActionResult _CustomerRezervationDetails()
        {

            return PartialView("_CustomerRezervationDetails");
        }
        public ActionResult frmEditCapacityHour()
        {
            return View();
        }
        public ActionResult frmRezervationDetails()
        {
            return View("frmRezervationDetails");
        }
        public ActionResult frmAppealDetails()
        {
            return View("frmAppealDetails");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ResetPassword(string password, string confirmPassword, string currentPassword)
        {
            var admin = _service.Get(x => x.Password == currentPassword);

            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                return Json(new { success = false, message = "Lütfen Boş Bırakmayınız." });
            }

            if (admin == null)
            {
                return Json(new { success = false, message = "Kullanılan Şifre Hatalı." });
            }

            if (password != confirmPassword)
            {
                return Json(new { success = false, message = "Şifreler Eşleşmiyor" });
            }

            admin.Password = password;
            _service.Update(admin);

            return Json(new { success = true, message = "Şifreniz başarıyla değiştirildi." });
        }

        public bool ConfirmRezervation(int id)
        {
            var tour = _context.Reservations.FirstOrDefault(t => t.Id == id);

            if (tour == null)
            {
                return false;
            }

            tour.Status = true;

            _context.SaveChanges();

            return true;
        }
        public bool CancelRezervation(int id)
        {
            var tour = _context.Reservations.FirstOrDefault(t => t.Id == id);

            if (tour == null)
            {
                return false;
            }

            tour.Status = false;

            _context.SaveChanges();

            return true;
        }
        [HttpPost]
        public JsonResult UpdateCustomerStatus(int id)
        {
            var customer = _context.Agencys.FirstOrDefault(t => t.Id == id);

            if (customer == null)
            {
                return Json(new { success = false, message = "Müşteri bulunamadı." });
            }

            if (customer.Status == true)
            {
                customer.Status = false;
            }
            else
            {
                customer.Status = true;
            }

            _context.SaveChanges();

            string message = customer.Status ? "Müşteri aktif edildi." : "Müşteri deaktif edildi.";

            return Json(new { success = true, message = message });
        }

        public bool ConfirmCustomer(int id)
        {
            var customer = _context.Agencys.FirstOrDefault(t => t.Id == id);

            if (customer == null)
            {
                return false;
            }

            customer.Status = true;
            customer.isApproved = true;



            _context.SaveChanges();

            return true;
        }
        public bool CancelCustomer(int id)
        {
            var customer = _context.Agencys.FirstOrDefault(t => t.Id == id);

            if (customer == null)
            {
                return false;
            }

            customer.isDeleted = true;
            customer.isApproved = false;

            _context.SaveChanges();

            return true;
        }
        [HttpPost]
        public JsonResult UpdateCapacity(HoursCapacity model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Model is null." });
            }

            var existingCapacity = _context.HoursCapacitys
                .FirstOrDefault(x => x.TourTypeId == model.TourTypeId && x.Hour == model.Hour);



            existingCapacity.Capacity = model.Capacity;

            _context.SaveChanges();

            return Json(new { success = true, message = "Successfully updated." });
        }
        public ActionResult getAgencies()
        {
            var agencies = _context.Agencys.Where(x => x.isApproved == true).ToList();

            if (!agencies.Any())
            {
                return new CustomJsonResult
                {
                    Data = new { success = false, message = "No agencies found." },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new CustomJsonResult
            {
                Data = new { success = true, Data = agencies },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public JsonResult createNewAgency(Agency model)
        {
            _context.Agencys.Add(model);

            int rowsAffected = _context.SaveChanges();

            if (rowsAffected > 0)
            {
                return Json(new { success = true, message = "Ajans başarı ile eklendi." });
            }
            else
            {
                return Json(new { success = false, message = "Ajans ekklenemedi." });
            }
        }
        [HttpGet]
        public JsonResult getHours(string tourType)
        {
            var result = (from h in _context.HoursCapacitys
                          join t in _context.TourTypes on h.TourTypeId equals t.Id
                          where tourType == "0" || h.TourTypeId == tourType
                          select new
                          {
                              h.Id,
                              h.Status,
                              t.Name,
                              h.Hour,
                              h.Capacity,
                              h.TourTypeId
                          }).ToList();



            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult updateStatusHour(int id)
        {
            var hour = _context.HoursCapacitys.Where(x => x.Id == id).FirstOrDefault();
            if (hour.Status == true)
            {
                hour.Status = false;
                _context.SaveChanges();
                return Json(new { succes = true, message = "Saat Deaktif edildi." });

            }
            else
            {
                hour.Status = true;
                _context.SaveChanges();
                return Json(new { succes = true, message = "Saat aktif edildi." });

            }

        }
        public JsonResult ChangePassword(string UserName, string newPassword, string confirmPassword)
        {


            var currentUserName = Session["UserName"]?.ToString();
            var admin = _context.Admins.Where(a => a.UserName == currentUserName).FirstOrDefault();

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(UserName))
            {
                return Json(new { success = false, message = "Lütfen Boş Bırakmayınız." });
            }

            if (UserName != currentUserName)
            {
                return Json(new { success = false, message = "Hatalı E-Mail Adresi." });
            }

            if (newPassword != confirmPassword)
            {
                return Json(new { success = false, message = "Yeni şifre ile şifre onayı eşleşmiyor." });
            }

            admin.Password = newPassword;
            admin.UserName = UserName;

            _context.SaveChanges();

            return Json(new { success = true, message = "Şifreniz başarıyla değiştirildi." });
        }




        public JsonResult GetRezervation(string TourTypeId, int Hour, string ReservationDate)
        {
            var reservations = _context.Database.SqlQuery<ReservationViewModel>(
                 "EXEC sp_GetReservations @TourTypeId, @Hour, @ReservationDate",
                 new SqlParameter("@TourTypeId", TourTypeId),
                 new SqlParameter("@Hour", Hour),
                 new SqlParameter("@ReservationDate", ReservationDate ?? (object)DBNull.Value)
                    ).ToList();


            return new CustomJsonResult { Data = reservations };

        }

        public JsonResult GetUnaprrovedAgencies()
        {
            var unaprrovedAgencies = _context.Agencys
        .Where(t => t.isDeleted == false && t.Status == false && t.isApproved == false)
        .ToList();

            return new CustomJsonResult
            {
                Data = new { success = true, Data = unaprrovedAgencies },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult CreateSpecificDateCapacity(SpecificDateCapacity model)
        {
            var isExist = _context.SpecificDateCapacitys.Any(x => x.Day == model.Day && x.Hour == model.Hour && x.TourTypeId == model.TourTypeId);
            if (isExist)
            {
                return Json(new { success = false, message = "Bu tarihte zaten bir kayıt var." });
            }
            else
            {
                var record = new SpecificDateCapacity
                {
                    TourTypeId = model.TourTypeId,
                    Day = model.Day,
                    Hour = model.Hour,
                    Capacity = model.Capacity,
                    Status = true,
                };

                _context.SpecificDateCapacitys.Add(record);
                _context.SaveChanges();
                return Json(new { success = true, message = "Kayıt başarıyla eklendi." });
            }

        }
        public JsonResult GetSpecificDateCapacity(string tourType)
        {

            var SpecificHours = (from s in _context.SpecificDateCapacitys
                                 join t in _context.TourTypes on s.TourTypeId equals t.Id
                                 where tourType == "0" || s.TourTypeId == tourType
                                 select new
                                 {
                                     s.Id,
                                     s.Status,
                                     t.Name,
                                     Date = s.Day + " " + s.Hour + ":00",
                                     s.Hour,
                                     s.Day,
                                     s.Capacity,
                                     s.TourTypeId
                                 }).ToList();
            return Json(SpecificHours, JsonRequestBehavior.AllowGet);

        }



        public JsonResult UpdateSpecificDateCapacity(SpecificDateCapacity model)
        {
            if (model == null)
            {
                return Json(new { success = false, message = "Model is null." });
            }

            var existingCapacity = _context.SpecificDateCapacitys
                .FirstOrDefault(x => x.TourTypeId == model.TourTypeId && x.Day == model.Day && x.Hour == model.Hour);



            existingCapacity.Capacity = model.Capacity;

            _context.SaveChanges();

            return Json(new { success = true, message = "Successfully updated." });
        }
        public JsonResult UpdateAgencyInformation(Agency model)
        {
            var agency = _context.Agencys.Where(x => x.Id == model.Id).FirstOrDefault();
            if (agency != null)
            {
                agency.AgencyName = model.AgencyName;
                agency.FirstName = model.FirstName;
                agency.LastName = model.LastName;
                agency.Phone = model.Phone;
                agency.Mail = model.Mail;
                agency.TaxNo = model.TaxNo;
                agency.TursabNo = model.TursabNo;
                agency.Tc = model.Tc;
                agency.Region = model.Region;
                _context.SaveChanges();
                return Json(new { success = true, message = "Başarıyla güncellendi." }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false, message = "Acenta bulunamadı." }, JsonRequestBehavior.AllowGet);

            }
        }
        [HttpGet]
        public JsonResult GetAgencyById(int AgencyId)
        {
            var agency = _context.Agencys.Where(x => x.Id == AgencyId).FirstOrDefault();
            return Json(agency,JsonRequestBehavior.AllowGet);
        } 
        [HttpPost]
        public JsonResult UpdateClockStatus(int Id)
        {
            var record = _context.SpecificDateCapacitys.Where(x => x.Id == Id).FirstOrDefault();
            if (record.Status==false)
            {
                record.Status = true;
                _context.SaveChanges();
                return Json(new { success = true, message = "Saat aktif edildi." }, JsonRequestBehavior.AllowGet);

            }
            else
            {
                record.Status = false;
                _context.SaveChanges();
                return Json(new { success = true, message = "Saat Deaktif edildi." }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}