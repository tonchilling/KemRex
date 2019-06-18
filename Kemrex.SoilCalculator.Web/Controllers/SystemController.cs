using Kemrex.SoilCalculator.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class SystemController : BaseController
    {
        public JsonResult GetStateList(string q = "")
        {
            var data = (from t in db.SysSubDistrict
                        join d in db.SysDistrict on t.DistrictId equals d.DistrictId
                        join p in db.SysState on d.StateId equals p.StateId
                        where
                            (
                                q == ""
                                || t.SubDistrictNameTH.Contains(q)
                                || d.DistrictNameTH.Contains(q)
                                || p.StateNameTH.Contains(q)
                            )
                        select new Select2Model()
                        {
                            id = t.SubDistrictId.ToString(),
                            text = string.Concat(t.SubDistrictNameTH, " > ", d.DistrictNameTH, " > ", p.StateNameTH)
                        });
            List<Select2Model> lst = data.Count() <= 0 ? new List<Select2Model>() : data.ToList();
            return Json(new { items = lst }, JsonRequestBehavior.AllowGet);
        }
    }
}