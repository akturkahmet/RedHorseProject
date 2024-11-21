﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RedHorseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
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
        public ActionResult ResetPassword()
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

    }
}