using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using RedHorseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;

namespace RedHorseProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAgencyService _service;

        public LoginController(IAgencyService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Admin p)
        {
            RedHorseContext c = new RedHorseContext();
            var adminuserinfo = c.Admins.FirstOrDefault
                (x => x.UserName == p.UserName
                && x.Password == p.Password);

            if (string.IsNullOrEmpty(p.UserName) || string.IsNullOrEmpty(p.Password))
            {
                ViewBag.ErrorMessage = "Kullanıcı adı ve şifre alanları boş bırakılamaz.";
                return View();
            }
            if (adminuserinfo == null)
            {
                ViewBag.ErrorMessage = "Geçersiz giriş bilgileri. Lütfen tekrar deneyin.";
                return View();
            }
            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.Role, false);
                Session["Role"] = adminuserinfo.Role;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AgencyLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgencyLogin(Agency p)
        {
            RedHorseContext c = new RedHorseContext();
            if (string.IsNullOrEmpty(p.UserName) || string.IsNullOrEmpty(p.Password))
            {
                ViewBag.ErrorMessage = "Kullanıcı adı ve şifre alanları boş bırakılamaz.";
                return View();
            }

            var adminuserinfo = c.Agencys.FirstOrDefault
                (x => x.UserName == p.UserName
                && x.Password == p.Password);

            if (adminuserinfo == null)
            {
                ViewBag.ErrorMessage = "Geçersiz giriş bilgileri. Lütfen tekrar deneyin.";
                return View();
            }

            if (adminuserinfo.Status == false)
            {
                ViewBag.ErrorMessage = "Başvurunuz henüz onaylanmamıştır.";
                return View();
            }

            FormsAuthentication.SetAuthCookie(adminuserinfo.Role, false);
            Session["Role"] = adminuserinfo.Role;
            Session["Mail"] = adminuserinfo.Mail;
            Session["AgencyId"] = adminuserinfo.Id;
            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Register()
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
                Status = false,
                CreatedDate = DateTime.Now
            };

            _service.Insert(newAgency);

            return Json(new { success = true, message = "Kayıt Başarılı!" });
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear(); 
            return RedirectToAction("AgencyLogin", "Login");
        }

    }
}