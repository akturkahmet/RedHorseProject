using DataAccessLayer.Context;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;

namespace RedHorseProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
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
            var adminuserinfo = c.Agencys.FirstOrDefault
                (x => x.UserName == p.UserName
                && x.Password == p.Password);

            if (adminuserinfo != null)
            {
                FormsAuthentication.SetAuthCookie(adminuserinfo.Role, false);
                Session["Role"] = adminuserinfo.Role;
                Session["Mail"] = adminuserinfo.Mail;
                Session["AgencyId"] = adminuserinfo.Id;
                return RedirectToAction("Index", "Customer");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Register()
        {
            return View();
        }
    }
}