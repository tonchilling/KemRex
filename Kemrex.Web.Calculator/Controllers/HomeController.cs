using Kemrex.Web.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Calculator.Controllers
{
    public class HomeController : KemrexController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}