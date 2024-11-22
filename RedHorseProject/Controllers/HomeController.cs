using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace RedHorseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IAdminService _service;

        public HomeController(IAdminService service)
        {
            _service = service;
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