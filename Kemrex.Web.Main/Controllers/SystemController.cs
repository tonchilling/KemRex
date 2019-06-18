using Kemrex.Core.Common.Models;
using Kemrex.Web.Common.Controllers;
using Kemrex.Web.Common.Models.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Main.Controllers
{
    public class SystemController : KemrexController
    {
        public JsonResult GetStateList(string q = "")
        {
            List<Select2Model> lst = uow.Modules.System.DropDownStateList(q);
            return Json(new { items = lst }, JsonRequestBehavior.AllowGet);
        }
    }
}