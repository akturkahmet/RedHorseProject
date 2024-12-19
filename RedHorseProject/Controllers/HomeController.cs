using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using RedHorseProject.Helper;
using RedHorseProject.Models;
using System;
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




        public JsonResult GetRezervation(string TourTypeId, int Hour)
        {
            var reservations = (from r in _context.Reservations
                                join a in _context.Agencys on r.Agency_Id equals a.Id
                                join t in _context.TourTypes on r.TourType equals t.Id
                                where
                                    (TourTypeId == "-1" || r.TourType == TourTypeId) &&
                                    (Hour == -1 || r.ReservationDate.Hour == Hour)
                                select new
                                {
                                    r.Id,
                                    r.FirstName,
                                    r.LastName,
                                    r.Phone,
                                    r.HotelName,
                                    r.PassportNo,
                                    r.HotelRoomNo,
                                    r.CustomerCount,
                                    r.ReservationDate,
                                    r.Status,
                                    a.AgencyName,
                                    t.Name
                                }).ToList();

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




    }
}