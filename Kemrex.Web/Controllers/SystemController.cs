using DMN.Standard.Common.Constraints;
using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common.Models;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.ActionFilters;
using Kemrex.Web.Common.Constraints;
using Kemrex.Web.Common.Models;
using Kemrex.Web.Controllers.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Controllers
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