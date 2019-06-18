using Kemrex.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Controllers
{
    public class AuthenController : KemrexController
    {
        // GET: Authen
        public ActionResult Index()
        {
            return View();
        }
    }
}