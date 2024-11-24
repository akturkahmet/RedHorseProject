using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using RedHorseProject.Models;
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

        public HomeController(IAdminService service, IAgencyService agencyService)
        {
            _service = service;
            _agencyService = agencyService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Rezervation()
        {
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
    }
}