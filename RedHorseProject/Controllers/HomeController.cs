using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using RedHorseProject.Models;
using RedHorseProject.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Services.Description;

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
            .Where(t => t.isDeleted == false && t.Status == true)
            .ToList();

            return View(list);
        }

        public ActionResult About()
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
                UserName = model.UserName,
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
            var list = _context.Agencys
            .Where(t => t.isDeleted == false && t.Status == false)
            .ToList();

            return View(list);
        }
        public ActionResult _CustomerDetails()
        {

            return PartialView("_CustomerDetails");
        }
        public ActionResult _CustomerRezervationDetails()
        {

            return PartialView("_CustomerRezervationDetails");
        }
        public ActionResult frmCustomerDetails()
        {
            return View("frmCustomerDetails");
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
        public bool DeleteCustomer(int id)
        {
            var customer = _context.Agencys.FirstOrDefault(t => t.Id == id);

            if (customer == null)
            {
                return false;
            }

            customer.isDeleted = true;

            _context.SaveChanges();

            return true;
        }
        public bool ConfirmCustomer(int id)
        {
            var customer = _context.Agencys.FirstOrDefault(t => t.Id == id);

            if (customer == null)
            {
                return false;
            }

            customer.Status = true;

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

            _context.SaveChanges();

            return true;
        }

    }
}