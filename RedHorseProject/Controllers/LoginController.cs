using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using EntityLayer.Concrete;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace RedHorseProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IAgencyService _service;
        RedHorseContext _context = new RedHorseContext();

        public LoginController(IAgencyService service)
        {
            _service = service;
        }
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword);
            return hashedEnteredPassword == storedHash;
        }
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Admin(Admin p)
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
                Session["UserName"] = adminuserinfo.UserName;
                Session["FirstName"] = adminuserinfo.FirstName;
                Session["LastName"] = adminuserinfo.LastName;
                Session["AdminId"] = adminuserinfo.Id;
                Session["FullName"] = $"{adminuserinfo.FirstName} {adminuserinfo.LastName}";

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View("AgencyLogin");
        }
        [HttpPost]
        public ActionResult AgencyLogin(Agency p)
        {
            RedHorseContext c = new RedHorseContext();

            if (string.IsNullOrEmpty(p.Mail) || string.IsNullOrEmpty(p.Password))
            {
                ViewBag.ErrorMessage = "Kullanıcı adı ve şifre alanları boş bırakılamaz.";
                return View();
            }

            var adminuserinfo = c.Agencys.FirstOrDefault(x => x.Mail == p.Mail);

            if (adminuserinfo == null || !VerifyPassword(p.Password, adminuserinfo.Password))
            {
                ViewBag.ErrorMessage = "Geçersiz giriş bilgileri. Lütfen tekrar deneyin.";
                return View();
            }

            if (adminuserinfo.Status == false)
            {
                ViewBag.ErrorMessage = "Başvurunuz henüz onaylanmamıştır.";
                return View();
            }

            // Kullanıcıyı oturum aç
            FormsAuthentication.SetAuthCookie(adminuserinfo.Role, false);
            Session["Role"] = adminuserinfo.Role;
            Session["Mail"] = adminuserinfo.Mail;
            Session["AgencyId"] = adminuserinfo.Id;
            Session["UserId"] = adminuserinfo.Id;
            Session["FullName"] = $"{adminuserinfo.FirstName} {adminuserinfo.LastName}";

            return RedirectToAction("Index", "Customer");
        }


        public ActionResult Register()
        {
            return View();
        }
        public ActionResult frmForgotPassword()
        {
            return View();
        } 
        public ActionResult frmEmailVerification()
        {
            return View();
        }



        public JsonResult CreateCustomer(string FirstName, string LastName, string AgencyName, string Phone, string Email, string Password, string ConfirmPassword, string TursabNo, string Region, string IdentityNo, string TaxNo)
        {
            if (Password != ConfirmPassword)
            {
                return Json(new { success = false, message = "Şifreler eşleşmiyor." });
            }

            var agency = _context.Agencys.FirstOrDefault(a => a.Mail == Email);

            if (agency != null)
            {
                return Json(new { success = false, message = "Email adresi zaten kayıtlı." });
            }

            string hashedPassword = HashPassword(Password);

            var newAgency = new Agency
            {
                FirstName = FirstName,
                LastName = LastName,
                AgencyName = AgencyName,
                Mail = Email,
                Phone = Phone,
                TursabNo = TursabNo,
                Tc = IdentityNo,
                Region = Region,
                Password = hashedPassword,
                TaxNo = TaxNo,
                Role = "Agency",
                Status = false,
                CreatedDate = DateTime.Now
            };

            _service.Insert(newAgency);

            return Json(new { success = true, message = "Başvurunuz alındı." });
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
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            var role = Session["Role"] as string;
            Session.Clear();
            if (role == "Admin")
            {
                return RedirectToAction("Admin", "Login");

            }
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]
        public JsonResult ChangePassword(string email, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                return Json(new { success = false, message = "Lütfen tüm alanları doldurunuz." });
            }

            var agency = _context.Agencys.FirstOrDefault(x => x.Mail == email);
            if (agency == null)
            {
                return Json(new { success = false, message = "Mail adresi sistemde bulunamadı." });
            }

            if (newPassword != confirmPassword)
            {
                return Json(new { success = false, message = "Yeni şifre ile şifre onayı eşleşmiyor." });
            }


            agency.Password = HashPassword(newPassword);

            _context.SaveChanges();

            return Json(new { success = true, message = "Şifreniz başarıyla değiştirildi." });
        }





        public JsonResult SendVerificationCode(string userEmail)
        {
            Random rand = new Random();
            int verificationCode = rand.Next(100000, 999999); 
            string code = verificationCode.ToString();

            string subject = "Doğrulama Kodu";
            string body = $"Merhaba,\n\nDoğrulama kodunuz: {code}\n\nBu kodu girerek işleminizi tamamlayabilirsiniz.";

            EmailHelper.SendEmail(userEmail, subject, body);

            Session["VerificationCode"] = code;

            return Json(new { success = true, message = "Doğrulama kodu e-posta adresinize gönderildi." });
        }
        public ActionResult VerifyCode(string userCode)
        {
            var correctCode = Session["VerificationCode"] as string;

            if (correctCode == userCode)
            {
                return Content("Doğrulama başarılı.");
            }
            else
            {
                return Content("Doğrulama kodu hatalı.");
            }
        }
    }
}