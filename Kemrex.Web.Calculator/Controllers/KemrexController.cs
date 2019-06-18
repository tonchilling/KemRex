using DMN.Standard.Common.Extensions;
using Kemrex.Core.Common;
using Kemrex.Core.Common.Constraints;
using Kemrex.Core.Common.Helper;
using Kemrex.Core.Database.Models;
using Kemrex.Web.Common.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kemrex.Web.Calculator.Controllers
{
    public class KemrexController : BaseController
    {
        private readonly int SiteId;
        public UnitOfWork uow;
        public int CurrentUID
        { get { return SystemHelper.GetConfigurationKey(ConfigKey.DEBUG_TESTMODE).ParseBoolean() ? 1 : Session["sid"].Convert2String().ParseInt(1); } } // For development only
        public DateTime CurrentDate { get { return DateTime.Now; } }
        public KemrexController()
        {
            string constr = SystemHelper.GetConfigurationKey(ConfigKey.CONNECTION_DB);
            uow = new UnitOfWork(constr);
            SiteId = SystemHelper.GetConfigurationKey(ConfigKey.APP_SITE_ID).ParseInt();
        }

        public ActionResult MenuSystem(string name = "_lyMenu")
        {
            List<SysMenu> md = uow.Modules.System.GetMenuBase(SiteId);
            return PartialView(name, md);
        }

        #region Business Function
        public string CustomerAddressText(TblCustomerAddress ob)
        {
            if ((ob.SubDistrict == null && ob.SubDistrictId > 0) || (ob.SubDistrictId > 0 && ob.SubDistrict.SubDistrictId != ob.SubDistrictId))
            { ob.SubDistrict = uow.Modules.SubDistrict.Get(ob.SubDistrictId.Value); }
            return ob.AddressText;
        }
        #endregion
    }
}