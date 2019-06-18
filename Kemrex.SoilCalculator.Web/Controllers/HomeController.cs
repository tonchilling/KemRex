using DMN.Standard.Common.Extensions;
using Kemrex.Database;
using Kemrex.FWCommon;
using Kemrex.SoilCalculator.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.SoilCalculator.Web.Controllers
{
    public class HomeController : BaseController
    {
        public UnitOfWork uow;
        public int CurrentUID => (Session["sid"].Convert2String().ParseIntNullable() ?? 0);
        public SysAccount CurrentUser => uow.Account.Get(CurrentUID);
        public DateTime CurrentDateTime = DateTime.Now;
        public HomeController()
        {
            uow = new UnitOfWork();
        }
        [BackendAuthorized]
        public ActionResult Index()
        {
            return View();
        }
    }
}