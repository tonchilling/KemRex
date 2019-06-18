using Kemrex.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Controllers
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