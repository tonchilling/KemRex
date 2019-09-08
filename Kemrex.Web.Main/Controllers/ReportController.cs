using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Kemrex.Core.Common.Helper;

namespace Kemrex.Web.Main.Controllers
{
    public class ReportController : KemrexController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
    }
}