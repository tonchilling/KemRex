using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class HomeController : KemrexController
    {
        [Authorized]
        public ActionResult Index()
        {
            return View();
        }
    }
}