using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedHorseProject.Controllers
{
    public class CustomerController : Controller
    {
        //GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AtvTurlari()
        {
            return View();
        }
        public ActionResult AtTurlari()
        {
            return View();

        }
        public ActionResult BalonTurlari()
        {
            return View();
        }
        public ActionResult DeveTurlari()
        {
            return View();
        }
        public ActionResult JeepSafariTurlari()
        {
            return View();
        }
        public ActionResult CustomerRezervation()
        {
            return View();
        }
        public ActionResult _Calendar()
        {
            
            return PartialView("_Calendar");
        }
        public ActionResult CustomerSettings()
        {
            return View();
        }

    }
}